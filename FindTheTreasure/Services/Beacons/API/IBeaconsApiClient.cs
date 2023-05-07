using FindTheTreasure.Models;
using Refit;

namespace FindTheTreasure.Services.Beacons.API
{
    public interface IBeaconsApiClient
    {
        [Get("/game/foundbeacons")]
        Task<IEnumerable<BeaconModel>> GetFoundBeaconsForUserAndGameAsync(string userName, string gameId);


    }
}
