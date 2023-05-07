using Android.App;
using FindTheTreasure.Models;
using FindTheTreasure.Services.Game.API;
using FindTheTreasure.Services.User.API;

namespace FindTheTreasure.Services.Game
{
    public class GameService
    {
        private readonly IGameApiClient _gameApiClient;      

        public GameService(IGameApiClient gameApiClient)
        {
            _gameApiClient = gameApiClient;
        }

        public async Task<bool> AddBeaconToGameAsync(int gameId, int beaconId)
        {
            return await _gameApiClient.AddBeaconToGameAsync(gameId, beaconId);
        }

        public async Task<List<GameModel>> GetGamesAsync()
        {
            return await _gameApiClient.GetGamesAsync();
        }

        public async Task<int> CreateGameAsync(CreateGameModel gameModel)
        {
            try
            {
                return await _gameApiClient.CreateGameAsync(gameModel);
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.Source);
                Debug.WriteLine(ex.ToString());
            }
            return await _gameApiClient.CreateGameAsync(gameModel);
        }

        public async Task<bool> StartGameAsync(string gameId)
        {
            // ToDo API

            Preferences.Set("isInGame", "true");
            Preferences.Set("gameId", gameId);
            return true;
        }

        public async Task<bool> StopGameAsync()
        {
            // ToDo API

            Preferences.Set("isInGame", "false");
            return true;
        }

        public bool IsInGame()
        {
            return Preferences.Get("isInGame", null) == "true";
        }

        public string GetGameId()
        {
            return Preferences.Get("gameId", null);
        }
    }
}
