using FindTheTreasure.Pages.ScoreBoard.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheTreasure.Services.ScoreBoard.API
{
    public interface IScoreBoardApiClient
    {
        [Get("/api/Game/ScoreBoard/{gameId}")]
        Task<IEnumerable<Score>> GetScoreForGameASync(int gameId);
    }
}
