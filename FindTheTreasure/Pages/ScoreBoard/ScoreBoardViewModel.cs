using FindTheTreasure.Pages.ScoreBoard.Models;
using FindTheTreasure.Services.ScoreBoard;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FindTheTreasure.Pages.ScoreBoard
{
    public class ScoreBoardViewModel
    {
        private readonly ScoreBoardService _scoreBoardService;
        private ICollection<Score> _scoreBoard;

        public event PropertyChangedEventHandler PropertyChanged;

        public ScoreBoardViewModel(ScoreBoardService scoreBoardService)
        {
            _scoreBoardService = scoreBoardService;
            Scoreboard = _scoreBoardService.GetScoreBoard(1).Result;
        }

        public ICollection<Score> Scoreboard
        {
            get => _scoreBoard;
            set { _scoreBoard = value; OnPropertyChanged(); }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
