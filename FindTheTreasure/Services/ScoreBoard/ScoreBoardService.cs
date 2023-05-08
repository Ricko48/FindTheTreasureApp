using FindTheTreasure.Pages.ScoreBoard.Models;
using FindTheTreasure.Services.ScoreBoard.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheTreasure.Services.ScoreBoard
{
    public class ScoreBoardService
    {
        private readonly IScoreBoardApiClient _scoreBoardApiClient;

        public ScoreBoardService(IScoreBoardApiClient scoreBoardApiClient) => _scoreBoardApiClient = scoreBoardApiClient;

        public async Task<ICollection<Score>> GetScoreBoard(int gameId)
        {
            try
            {
                return (await _scoreBoardApiClient.GetScoreForGameASync(gameId)).ToList();
            }
            catch (Exception e) { 
                Console.Write(e.ToString());
                return new List<Score>();
            }
        }
    }
}
