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
            using var dbContext = new TreasureDbContext();
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

        [HttpGet("{id}")]
        public User? GetUser(int id)
        {
            using var dbContext = new TreasureDbContext();
            return dbContext.Users.Find(id);
        }

        [HttpGet("exists/{id}")]
        public bool UserExists(int id)
        {
            using var dbContext = new TreasureDbContext();
            return dbContext.Users.Find(id) == null;
        }
    }
}
