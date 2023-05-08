using FindTheTreasure.Pages.ScoreBoard.Models;
using FindTheTreasure.Services.Game.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheTreasure.Services.ScoreBoard
{
    public class ScoreBoardService
    {
        private readonly IGameApiClient _gameApiClient;

        public ScoreBoardService(IGameApiClient gameApiClient)
        {
            _gameApiClient = gameApiClient;
        }
        public async Task<List<Score>> GetScoreBoard(int gameId)
        {
            try
            {
                return (await _gameApiClient.GetScoreForGameASync(gameId)).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.Source);
                Debug.WriteLine(ex.ToString());
            }
            return new List<Score>();
        }
    }
}
