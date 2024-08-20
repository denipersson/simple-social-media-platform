using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TweetService.Models;
using TweetService.Repositories;

namespace TweetService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        private readonly ITweetRepository _tweetRepository;

        public TweetsController(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }

        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTweetsByUserId(Guid userId)
        {
            var tweets = await _tweetRepository.GetTweetsByUserIdAsync(userId);
            return Ok(tweets);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTweetById(Guid id)
        {
            var tweet = await _tweetRepository.GetTweetByIdAsync(id);
            if (tweet == null)
            {
                return NotFound();
            }
            return Ok(tweet);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTweet(Tweet tweet)
        {
            tweet.Id = Guid.NewGuid();
            tweet.UserId = Guid.Parse(User.Identity.Name); // Assuming User.Identity.Name contains the userId
            tweet.CreatedAt = DateTime.UtcNow;

            await _tweetRepository.CreateTweetAsync(tweet);
            return CreatedAtAction(nameof(GetTweetById), new { id = tweet.Id }, tweet);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTweet(Guid id)
        {
            var tweet = await _tweetRepository.GetTweetByIdAsync(id);
            if (tweet == null)
            {
                return NotFound();
            }

            await _tweetRepository.DeleteTweetAsync(id);
            return NoContent();
        }
    }
}
