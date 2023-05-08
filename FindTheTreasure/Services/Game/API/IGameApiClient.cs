
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindTheTreasure.Models;
using FindTheTreasure.Pages.ScoreBoard.Models;

using Refit;

namespace FindTheTreasure.Services.Game.API
{
    public interface IGameApiClient
    {
        [Post("/api/game/{gameId}/beacon/{beaconId}")]
        Task<bool> AddBeaconToGameAsync(int gameId, int beaconId);

        [Post("/api/game")]
        Task<int> CreateGameAsync(CreateGameModel gameModel);

        [Get("/api/game")]
        Task<List<GameModel>> GetGamesAsync();


        [Post("/api/game/{gameId}/user/{userId}")]
        Task<int> StartGame(int gameId, int userId);

        [Get("/api/Game/ScoreBoards")]
        Task<IEnumerable<Scoreboard>> GetScoreBoardsAsync();

    }
}
