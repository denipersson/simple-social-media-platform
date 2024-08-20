using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TweetService.Models;

namespace TweetService.Data
{
    public class TweetContext : DbContext
    {
        public TweetContext(DbContextOptions<TweetContext> options) : base(options) { }

        public DbSet<Tweet> Tweets { get; set; }
    }
}
