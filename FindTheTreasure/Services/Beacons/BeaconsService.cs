using Android.Gms.Common.Apis;
using FindTheTreasure.Models;
using FindTheTreasure.Services.Beacons.API;
using FindTheTreasure.Services.Game;
using FindTheTreasure.Services.User;

namespace FindTheTreasure.Services.Beacons
{
    public class BeaconsService
    {
        private readonly IBeaconsApiClient _beaconsApiClient;
        private readonly UserService _userService;
        private readonly GameService _gameService;

        public BeaconsService(IBeaconsApiClient beaconsApiClient, UserService userService, GameService gameService)
        {
            _beaconsApiClient = beaconsApiClient;
            _userService = userService;
            _gameService = gameService;
        }

        public async Task<UpdateBeaconModel> UpdateBeacon(UpdateBeaconModel model)
        {
            return await _beaconsApiClient.UpdateBeacon(model);
        }

        public async Task<IEnumerable<BeaconModel>> GetFoundBeaconsAsync()
        {
            if (!_userService.IsSignedIn() || !_gameService.IsInGame())
            {
                return new List<BeaconModel>();
            }

            var participantId = _userService.GetUser().ParticipantId;
            return await _beaconsApiClient.GetFoundBeaconsForParticipant(participantId.Value);
        }

        public async Task<IEnumerable<BeaconModel>> GetAllAsync()
        {
            return await _beaconsApiClient.GetAllAsync();
        }

        public async Task<GameBeacon> GetNextBeaconInGame()
        {
            var r = Preferences.Get("gameId", null);
            var gameId = int.Parse(r);
            var beaconOrder = int.Parse(Preferences.Get("beaconOrder", null)) + 1;
            Preferences.Set("beaconOrder", beaconOrder.ToString());
            return await _beaconsApiClient.GetBeaconWithOrder(gameId, beaconOrder);
        }

        public async Task SetBeaconToFoundAsync(int beaconId)
        {
            var participantId = _userService.GetUser().ParticipantId;
            await _beaconsApiClient.SetParticipantBeaconFound(participantId.Value, beaconId);
        }
    }
}
