using FindTheTreasure.Pages.ScoreBoard.Models;
using FindTheTreasure.Services.Game;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FindTheTreasure.Pages.ScoreBoard
{
    public class ScoreBoardViewModel
    {
        private readonly GameService _gameService;
        private ICollection<Score> _scoreBoard;

        public event PropertyChangedEventHandler PropertyChanged;

        public ScoreBoardViewModel(GameService gameService)
        {
            _gameService = gameService;
            Scoreboard = new List<Score>();
            FillScoreBoard();
        }

        private async void FillScoreBoard()
        {
            Scoreboard = await _gameService.GetScoreBoard(1);
        }


        public ICollection<Score> Scoreboard
        {
            get => _scoreBoard;
            set
            {
                _scoreBoard = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
