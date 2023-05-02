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

        public async Task<IEnumerable<BeaconModel>> GetFoundBeaconsAsync()
        {
            if (!_userService.IsSignedIn())
            {
                return new List<BeaconModel>();
            }

            await _gameService.StartGameAsync("123"); // just for testing

            if (!_gameService.IsInGame())
            {
                return new List<BeaconModel>();
            }

            var userName = _userService.GetUser().UserName;
            var gameId = _gameService.GetGameId();
            //return await _beaconsApiClient.GetFoundBeaconsForUserAndGameAsync(userName, gameId);
            
            return new List<BeaconModel>()  // for testing
            {
                new BeaconModel
                {
                    MAC = "0C:F3:EE:B8:DD:0A",
                    Name = "EMBeacon54828",
                    SerialNumber = "5350336951052",
                    Latitude = 50.367884,
                    Longitude = 14.570348
                },
                new BeaconModel
                {
                    MAC = "blabal",
                    Name = "Treasure 2",
                    SerialNumber = "123456",
                    Latitude = 39.366051,
                    Longitude = 31.088122
                }
            };
        }

        public IEnumerable<BeaconModel> GetAll()
        {
            return new List<BeaconModel>()  // for testing
            {
                new BeaconModel
                {
                    MAC = "0C:F3:EE:B8:DD:0A",
                    Name = "EMBeacon54828",
                    SerialNumber = "5350336951052"
                }
            };
        }
    }
}
