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
        public IActionResult CreateUser(User user)
        {
            using var dbContext = new TreasureDbContext();
            user.Id = user.Id == null || user.Id == 0 ? null : user.Id;
            if (dbContext.Users.Any(u => u.UserName == user.UserName))
            {
                return Conflict();
            }

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return Ok(user.Id);
        }

        [HttpGet("{username}")]
        public IActionResult GetUser(string username)
        {
            using var dbContext = new TreasureDbContext();
            var users = dbContext.Users.Where(x => x.UserName == username);

            if (!users.Any())
            {
                return NotFound();
            }
            
            return Ok(users.First());
        }

        [HttpDelete("delete/{username}")]
        public IActionResult DeleteUser(string username)
        {
            using var dbContext = new TreasureDbContext();
            var users = dbContext.Users.Where(x => x.UserName == username);

            if (!users.Any())
            {
                return NotFound();
            }

            dbContext.Users.Remove(users.First());
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet("all")]
        public IEnumerable<User> GetAll()
        {
            using var dbContext = new TreasureDbContext();
            return dbContext.Users.ToList();
        }
    }
}
