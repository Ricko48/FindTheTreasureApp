using FindTheTreasure.Models;
using Refit;

namespace FindTheTreasure.Services.Beacons.API
{
    public interface IBeaconsApiClient
    {
        [Get("/api/game/{gameId}/foundbeacons/{userName}")]
        Task<IEnumerable<BeaconModel>> GetFoundBeaconsForUserAndGameAsync(string userName, string gameId);

        [Get("/api/beacon/all")]
        Task<IEnumerable<BeaconModel>> GetAllAsync();
    }
}
