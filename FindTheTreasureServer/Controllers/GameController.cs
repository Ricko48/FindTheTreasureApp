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
            using var dbContext = new TreasureDbContext();
            var newBeacon = new GameBeacon { BeaconId = beaconId, GameId = gameId };
            dbContext.GameBeacons.Add(newBeacon);
            dbContext.GameParticipants
                .Where(p => p.GameId == gameId)
                .ForEachAsync(p => dbContext.ParticipantBeacons
                    .Add(new ParticipantBeacon
                    {
                        GameBeaconId = newBeacon.Id ?? 0,
                        GameParticipantId = p.Id ?? 0,
                        Found = false
                    }))
                .Wait();
            dbContext.SaveChanges();
            return true;
        }

        [HttpDelete("{gameId}/beacon/{beaconId}")]
        public bool DeleteBeaconFromGame(int gameId, int beaconId)
        {
            using var dbContext = new TreasureDbContext();
            var beacon = dbContext.GameBeacons.First(b => b.BeaconId == beaconId && b.GameId == gameId);
            if (beacon == null)
                return false;
            dbContext.GameBeacons.Remove(beacon);
            dbContext.SaveChanges();
            return true;
        }

        [HttpPost("{gameId}/user{userId}")]
        public bool AddUserToGame(int gameId, int userId)
        {
            using var dbContext = new TreasureDbContext();
            var newParticipant= new GameParticipant { UserId = userId, GameId = gameId };
            dbContext.GameParticipants.Add(newParticipant);
            dbContext.GameBeacons
                .Where(b => b.GameId == gameId)
                .ForEachAsync(b => dbContext.ParticipantBeacons
                    .Add(new ParticipantBeacon
                    {
                        GameBeaconId = b.Id ?? 0,
                        GameParticipantId = newParticipant.Id ?? 0,
                        Found = false
                    }))
                .Wait();
            dbContext.SaveChanges();
            return true;
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
