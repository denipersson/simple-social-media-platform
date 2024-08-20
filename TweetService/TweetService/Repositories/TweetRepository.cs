using Microsoft.EntityFrameworkCore;
using TweetService.Data;
using TweetService.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TweetService.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        private readonly TweetContext _context;

        public TweetRepository(TweetContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tweet>> GetTweetsByUserIdAsync(Guid userId)
        {
            return await _context.Tweets
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
        }

        public async Task<Tweet> GetTweetByIdAsync(Guid id)
        {
            return await _context.Tweets.FindAsync(id);
        }

        public async Task CreateTweetAsync(Tweet tweet)
        {
            await _context.Tweets.AddAsync(tweet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTweetAsync(Guid id)
        {
            var tweet = await _context.Tweets.FindAsync(id);
            if (tweet != null)
            {
                _context.Tweets.Remove(tweet);
                await _context.SaveChangesAsync();
            }
        }
    }
}
