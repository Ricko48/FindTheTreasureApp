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

            var userId = _userService.GetUser().Id;
            return await _beaconsApiClient.GetFoundBeaconsForParticipant(userId);
        }

        public async Task<IEnumerable<BeaconModel>> GetAllAsync()
        {
            return await _beaconsApiClient.GetAllAsync();
        }
    }
}
