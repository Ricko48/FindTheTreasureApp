using FindTheTreasureServer.Database;
using FindTheTreasureServer.Database.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindTheTreasureServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Game> GetGames()
        {
            using var dbContext = new TreasureDbContext();
            return dbContext.Games;
        }

        [HttpPut]
        public int UpdateGame(Game game)
        {
            using var dbContext = new TreasureDbContext();
            dbContext.Games.Update(game);
            dbContext.SaveChanges();
            return game.Id ?? 0;
        }

        [HttpPost]
        public int CreateGame(Game game)
        {
            using var dbContext = new TreasureDbContext();
            dbContext.Games.Add(game);
            dbContext.SaveChanges();
            return game.Id ?? 0;
        }

        [HttpGet("{id}")]
        public Game? GetGame(int id)
        {
            using var dbContext = new TreasureDbContext();
            return dbContext.Games.Find(id);
        }

        [HttpPost("{gameId}/beacon/{beaconId}")]
        public bool AddBeaconToGame(int gameId, int beaconId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{gameId}/beacon/{beaconId}")]
        public bool DeleteBeaconFromGame(int gameId, int beaconId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{gameId}/user{userId}")]
        public bool AddUserToGame(int gameId, int userId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{gameId}/user/{userId}")]
        public bool DeletePlayerFromGame(int gameId, int userId)
        {
            using var dbContext = new TreasureDbContext();
            var participant = dbContext.GameParticipants.First(p => p.UserId == userId && p.GameId == gameId);
            if (participant == null)
                return false;
            dbContext.GameParticipants.Remove(participant);
            dbContext.SaveChanges();
            return true;
        }
    }
}
