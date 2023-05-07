using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindTheTreasure.Models;
using Refit;

namespace FindTheTreasure.Services.Game.API
{
    public interface IGameApiClient
    {
        [Post("/api/game/{gameId}/beacon/{beaconId}")]
        Task<bool> AddBeaconToGameAsync(int gameId, int beaconId);

        [Post("/api/game")]
        Task<int> CreateGameAsync(CreateGameModel gameModel);

        [Put("/api/game")]
        Task<List<GameModel>> GetGamesAsync();
    }
}
