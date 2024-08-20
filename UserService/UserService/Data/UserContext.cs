using UserService.Models;
using Microsoft.Data.Entity;
namespace UserService.Data
{
    public class UserContext : DbContext
    {
        public List<User> Users { get; set; }
    }
}
