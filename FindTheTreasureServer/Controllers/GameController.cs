using FindTheTreasureServer.Database;
using FindTheTreasureServer.Database.Entity;
using FindTheTreasureServer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindTheTreasureServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Game>> GetGames()
        {
            using var dbContext = new TreasureDbContext();
            return await dbContext.Games.ToListAsync();
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
        public async Task<int> CreateGame(Game game)
        {
            using var dbContext = new TreasureDbContext();
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();
            return game.Id ?? 0;
        }

        [HttpGet("{id}")]
        public Game? GetGame(int id)
        {
            using var dbContext = new TreasureDbContext();
            return dbContext.Games.Find(id);
        }

        /// <summary>
        /// Adds beacon to game
        /// </summary>
        /// <param name="gameId">Game Id</param>
        /// <param name="beaconId">Beacon Id</param>
        /// <returns>Returns true, if beacon added to game or false if beacon does not exist or it is already 
        /// assigned to game</returns>
        [HttpPost("{gameId}/beacon/{beaconId}")]
        public bool AddBeaconToGame(int gameId, int beaconId)
        {
            using var dbContext = new TreasureDbContext();
            var beacon = dbContext.Beacons.Find(beaconId);
            if (beacon == null || beacon.GameId != null)
                return false;
            beacon.GameId = gameId;
            beacon.Order = dbContext.Beacons.Count(b => b.GameId == gameId);
            var participantBeacons = dbContext
                .GameParticipants
                .Where(p => p.GameId == gameId)
                .Select(p => new ParticipantBeacon
                {
                    BeaconId = beaconId,
                    Found = false,
                    GameParticipantId = p.Id.Value,
                });
            dbContext.ParticipantBeacons.AddRange(participantBeacons);
            dbContext.SaveChanges();
            return true;
        }

        [HttpDelete("{gameId}/beacon/{beaconId}")]
        public bool DeleteBeaconFromGame(int gameId, int beaconId)
        {
            using var dbContext = new TreasureDbContext();
            var beacon = dbContext.Beacons.Find(beaconId);
            if (beacon.GameId != gameId)
                return false;
             dbContext.Beacons.Where(b => b.GameId == gameId && b.Order > beacon.Order)
                .ForEachAsync(b => --b.Order)
                .Wait();
            beacon.GameId = null;
            dbContext.SaveChanges();
            return true;
        }

        [HttpPost("{gameId}/user/{userId}")]
        public int AddUserToGame(int gameId, int userId)
        {
            using var dbContext = new TreasureDbContext();
            var participant = new GameParticipant { GameId = gameId, UserId = userId, Start = DateTime.Now };
            dbContext.GameParticipants.Add(participant);
            var beacons = dbContext
                .Beacons
                .Where(b => b.GameId == gameId)
                .Select(b => new ParticipantBeacon
                {
                    BeaconId = b.Id.Value,
                    GameParticipantId = participant.Id.Value,
                    Found = false
                });
            dbContext.ParticipantBeacons.AddRange(beacons);
            dbContext.SaveChanges();
            return participant.Id.Value;
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

        [HttpGet("ScoreBoards")]
        public IEnumerable<ScoreboardDTO> GetScoreBoardForGame()
        {
            using var dbContext = new TreasureDbContext();
            var games = dbContext.Games.ToList();
            var result = new List<ScoreboardDTO>();
            foreach (var game in games)
            {
                var scoreboard = new List<ScoreDTO>();
                var participants = dbContext.GameParticipants
                    .Where(p => p.GameId == game.Id)
                    .OrderBy(p => p.End.HasValue ? p.End - p.Start : TimeSpan.MaxValue);
                var position = 1;
                var tmpScoreboard = new List<ScoreDTO>();
                foreach (var p in participants)
                {
                    var user = dbContext.Users.Find(p.UserId);
                    tmpScoreboard.Add(new ScoreDTO
                    {
                        Position = position,
                        Time = p.End.HasValue ? (p.End - p.Start).ToString() : "DNF",
                        Username = user.UserName,
                    });
                    position++;
                }
                result.Add(new ScoreboardDTO { Scores = tmpScoreboard, GameName = game.Name });
            }
            return result;
        }
    }
}
