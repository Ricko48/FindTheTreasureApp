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
            return _beaconsApiClient.GetFoundBeaconsForParticipant(participantId.Value).Result;
        }

        public async Task<IEnumerable<BeaconModel>> GetAllAsync()
        {
            return await _beaconsApiClient.GetAllAsync();
        }

        public async Task<IEnumerable<BeaconModel>> GetNotAddedBeaconsForGame(int gameId)
        {
            return await _beaconsApiClient.GetAllNotAddedBeaconsInGame(gameId);
        }

        public async Task<GameBeacon> GetNextBeaconInGame()
        {
            var gameId = int.Parse(Preferences.Get("gameId", null));
            var beaconOrder = int.Parse(Preferences.Get("beaconOrder", null)) + 1;
            Preferences.Set("beaconOrder", beaconOrder.ToString());
            return _beaconsApiClient.GetBeaconWithOrder(gameId, beaconOrder).Result;
        }

        public async Task SetBeaconToFoundAsync(string mac)
        {
            var participantId = _userService.GetUser().ParticipantId;
            await _beaconsApiClient.SetParticipantBeaconFound(mac, participantId.Value );
        }
    }
}
