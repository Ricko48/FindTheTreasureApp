using FindTheTreasure.Services.Beacons.API;
﻿using Android.App;
using FindTheTreasure.Models;
using FindTheTreasure.Pages.ScoreBoard.Models;

using FindTheTreasure.Services.Game.API;
using FindTheTreasure.Services.User;

namespace FindTheTreasure.Services.Game
{
    public class GameService
    {
        private readonly IGameApiClient _gameApiClient;
        private readonly UserService _userService;

        public GameService(IGameApiClient gameApiClient, UserService userService)
        {
            _gameApiClient = gameApiClient;
            _userService = userService;
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
            }
            return 0;
        }

        public async Task StartGameAsync(int gameId)
        {
            var userId = _userService.GetUser().Id;
            var participantId = await _gameApiClient.StartGame(gameId, userId);

            Preferences.Set("isInGame", "true");
            Preferences.Set("gameId", gameId);
            Preferences.Set("beaconOder", (-1).ToString());
            Preferences.Set("participantId", participantId.ToString());
        }

        public void StopGame()
        {
            Preferences.Set("isInGame", "false");
        }

        public bool IsInGame()
        {
            var isInGame = Preferences.Get("isInGame", null);
            if (string.IsNullOrEmpty(isInGame))
            {
                Preferences.Set("isInGame", "false");
            }
            return isInGame == "true";
        }

        public string GetGameId()
        {
            return Preferences.Get("gameId", null);
        }

        public async Task<List<Scoreboard>> GetScoreBoards()
        {
            try
            {
                return (await _gameApiClient.GetScoreBoardsAsync()).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.Source);
                Debug.WriteLine(ex.ToString());
                await Shell.Current.DisplayAlert("Error", "Something went wrong.", "Ok");
            }
            return new List<Scoreboard>();
        }
    }
}
