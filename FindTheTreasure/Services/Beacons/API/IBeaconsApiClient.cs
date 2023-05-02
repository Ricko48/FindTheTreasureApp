using FindTheTreasure.Models;
using Refit;

namespace FindTheTreasure.Services.Beacons.API
{
    public interface IBeaconsApiClient
    {
        [Get("/game/foundbeacons")]
        public Task<IEnumerable<BeaconModel>> GetFoundBeaconsForUserAndGameAsync(string userName, string gameId);
    }
}
