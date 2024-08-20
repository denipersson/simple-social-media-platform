using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using UserService.Models;
using UserService.Repositories;
using UserService.Utilities;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UsersController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);
            if (existingUser != null)
            {
                return BadRequest("Username is already taken.");
            }

            user.Id = Guid.NewGuid();
            await _userRepository.CreateUserAsync(user);
            return Ok("Successfully created user. " + user.ToString());
        }
        //Get User By ID

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }
        //Get User By Username

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        //Login 

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginRequest)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginRequest.Username);
            if (user == null || user.Password != loginRequest.Password) // You should hash and compare passwords in production
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = JwtTokenHelper.GenerateJwtToken(
                user.Username,
                _configuration["Jwt:Key"],
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"]);

            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            // Extract the username from the claims
            var username = User.Identity.Name; 
            // Use the username to retrieve the user profile from the database
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

    }

}
