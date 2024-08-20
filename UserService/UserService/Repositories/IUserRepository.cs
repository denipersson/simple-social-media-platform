using UserService.Models;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByUsernameAsync(string username);
        Task CreateUserAsync(User user);
    }
}
