﻿using FindTheTreasureServer.Database;
using FindTheTreasureServer.Database.Entity;
using FindTheTreasureServer.DTO;
using Microsoft.AspNetCore.Mvc;

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
            return dbContext.Games.ToList();
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
            beacon.GameId = null;
            dbContext.SaveChanges();
            return true;
        }

        [HttpPost("{gameId}/user/{userId}")]
        public bool AddUserToGame(int gameId, int userId)
        {
            using var dbContext = new TreasureDbContext();
            var participant = new GameParticipant { GameId = gameId, UserId = userId };
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

        [HttpGet("ScoreBoard/{gameId}")]
        public IEnumerable<Score> GetScoreBoardForGame(int gameId)
        {
            using var dbContext = new TreasureDbContext();
            var participants = dbContext.GameParticipants
                .Where(p => p.GameId == gameId)
                .OrderBy(p => p.End.HasValue ? p.End - p.Start : TimeSpan.MaxValue);
            var position = 1;
            var result = new List<Score>();
            foreach (var p in participants)
            {
                var user = dbContext.Users.Find(p.UserId);
                result.Add(new Score
                {
                    Position = position,
                    Time = p.End.HasValue ? (p.End - p.Start).ToString() : "DNF",
                    Username = user.UserName,
                });
                position++;
            }

            return result;
        }
    }
}
