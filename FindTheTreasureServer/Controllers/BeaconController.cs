using FindTheTreasureServer.Database;
using FindTheTreasureServer.Database.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            dbContext.Games.Find(gameId);
            throw new NotImplementedException();
        }

        [HttpPut]
        public Beacon UpdateBeacon(Beacon beacon)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Beacon CreateBeacon(Beacon beacon)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public Beacon GetBeacon(int id)
        {
            throw new NotImplementedException();
        }
    }
}
