using System;
using System.ComponentModel.DataAnnotations;

namespace TweetService.Models
{
    public class Tweet
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(280)] // Twitter-like character limit
        public string Content { get; set; }

        [Required]
        public Guid UserId { get; set; } // This will link to the UserService

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
