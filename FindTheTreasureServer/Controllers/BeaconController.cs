﻿using FindTheTreasureServer.Database;
using FindTheTreasureServer.Database.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindTheTreasureServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeaconController : ControllerBase
    {
        [HttpGet("game/{GameId}")]
        public IEnumerable<Beacon> GetBeaconsForGame(int gameId)
        {
            using var dbContext = new TreasureDbContext();
            return dbContext.Beacons.Where(b => b.GameId == gameId);
        }

        [HttpGet("participant/found/{participantId}")]
        public IEnumerable<Beacon> GetFoundBeaconsForParticipant(int participantId)
        {
            using var dbContext = new TreasureDbContext();
            var participantBeacons = dbContext
                .ParticipantBeacons
                .Where(b => b.GameParticipantId == participantId && b.Found);
            return dbContext
                .Beacons
                .Where(b => participantBeacons.Any(pb => pb.BeaconId == b.Id))
                .ToList();
        }

        [HttpPut]
        public Beacon UpdateBeacon(Beacon beacon)
        {
            using var dbContext = new TreasureDbContext();
            dbContext.Beacons.Update(beacon);
            dbContext.SaveChanges();
            return beacon;
        }

        [HttpPost]
        public Beacon CreateBeacon(Beacon beacon)
        {
            using var dbContext = new TreasureDbContext();
            dbContext.Beacons.Add(beacon);
            dbContext.SaveChanges();
            return beacon;
        }

        [HttpGet("{id}")]
        public Beacon GetBeacon(int id)
        {
            using var dbContext = new TreasureDbContext();
            return dbContext.Beacons.Find(id);
        }

        /// <summary>
        /// Returns if beacon is used in a game
        /// </summary>
        /// <param name="beaconId">Id of a beacon</param>
        /// <returns>Returns 0 if it is not in the game or Id of the game</returns>
        [HttpGet("ingame/{beaconId}")]
        public int GetBeaconInGame(int beaconId)
        {
            using var dbContext = new TreasureDbContext();
            var beacon = dbContext.Beacons.Find(beaconId);
            return beacon.GameId.HasValue ? beacon.GameId.Value : 0;
        }

        [HttpPost("{mac}/{participantId}")]
        public void SetParticipantBeaconFound(string mac, int participantId)
        {
            using var dbContext = new TreasureDbContext();
            var ble = dbContext.Beacons.First(b => b.MacAddress == mac);
            var beacon = dbContext.ParticipantBeacons.First(b => b.GameParticipantId == participantId && b.BeaconId == ble.Id);
            beacon.Found = true;
            dbContext.SaveChanges();
            if (dbContext.ParticipantBeacons.Where(b => b.GameParticipantId == participantId).All(b => b.Found))
                dbContext.GameParticipants.Find(participantId).End = DateTime.Now;
            dbContext.SaveChanges();
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Beacon>> GetAllBeacons()
        {
            using var dbContext = new TreasureDbContext();
            return await dbContext.Beacons.ToListAsync();
        }

        [HttpGet("all/{gameId}")]
        public async Task<IEnumerable<Beacon>> GetAllNotAddedBeaconsInGame(int gameId)
        {
            using var dbContext = new TreasureDbContext();
            return await dbContext.Beacons.Where(x => x.GameId != gameId).ToListAsync();
        }

        [HttpGet("gameBeacon/{gameId}/{order}")]
        public IActionResult GetBeaconWithOrder(int gameId, int order)
        {
            using var dbContext = new TreasureDbContext();
            var ble = dbContext.Beacons.FirstOrDefault(b => b.GameId == gameId && b.Order == order);
            if(ble == null)
            {
                return NotFound();
            }
            return Ok(ble);
        }
    }
}
