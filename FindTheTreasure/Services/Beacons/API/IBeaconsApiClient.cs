using FindTheTreasure.Models;
using Refit;

namespace FindTheTreasure.Services.Beacons.API
{
    public interface IBeaconsApiClient
    {
        [Put("/api/beacon")]
        Task<UpdateBeaconModel> UpdateBeacon(UpdateBeaconModel beacon);

        [Get("/api/beacon/participant/found/{id}")]
        Task<IEnumerable<BeaconModel>> GetFoundBeaconsForParticipant(int id);

        [Get("/api/beacon/all")]
        Task<IEnumerable<BeaconModel>> GetAllAsync();

        [Post("/api/beacon/{mac}/{participantId}")]
        Task SetParticipantBeaconFound(string mac, int participantId);

        [Get("/api/beacon/gameBeacon/{gameId}/{order}")]
        Task<GameBeacon> GetBeaconWithOrder(int gameId, int order);

        [Get("/api/beacon/all/{gameId}")]
        Task<IEnumerable<BeaconModel>> GetAllNotAddedBeaconsInGame(int gameId);
    }
}
