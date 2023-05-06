using System.Diagnostics;
using FindTheTreasureServer.Database;
using FindTheTreasureServer.Database.Entity;
using Microsoft.AspNetCore.Mvc;

namespace FindTheTreasureServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public int CreateUser(User user)
        {
            Debug.WriteLine($"Create reqeust for '{user.UserName}'");
            using var dbContext = new TreasureDbContext();

            if (dbContext.Users.Any(u => u.UserName == user.UserName))
            {
                return -1;
            }

            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user.Id ?? 0;
        }

        [HttpPut]
        public int UpdateUser(User user)
        {
            using var dbContext = new TreasureDbContext();
            dbContext.Users.Update(user);
            dbContext.SaveChanges();
            return user.Id ?? 0;
        }

        [HttpGet("{username}")]
        public User? GetUser(string username)
        {
            Debug.WriteLine($"Get reqeust for '{username}'");
            using var dbContext = new TreasureDbContext();
            var users = dbContext.Users.Where(x => x.UserName == username);
            if (!users.Any())
            {
                return null;
            }
            return dbContext.Users.First();
        }

        [HttpDelete("delete/{username}")]
        public bool DeleteUser(string username)
        {
            using var dbContext = new TreasureDbContext();
            var users = dbContext.Users.Where(x => x.UserName == username);
            if (!users.Any())
            {
                Debug.WriteLine($"Delete reqeust for '{username}' failed");
                return false;
            }
            Debug.WriteLine($"Delete reqeust for '{username}' succeded");
            dbContext.Users.Remove(users.First());
            dbContext.SaveChanges();
            return true;
        }

        [HttpGet("all")]
        public IEnumerable<User> GetAll()
        {
            using var dbContext = new TreasureDbContext();
            return dbContext.Users.ToList();
        }
    }
}
