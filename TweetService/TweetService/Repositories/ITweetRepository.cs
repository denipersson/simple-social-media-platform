using TweetService.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TweetService.Repositories
{
    public interface ITweetRepository
    {
        Task<IEnumerable<Tweet>> GetTweetsByUserIdAsync(Guid userId);
        Task<Tweet> GetTweetByIdAsync(Guid id);
        Task CreateTweetAsync(Tweet tweet);
        Task DeleteTweetAsync(Guid id);
    }
}
