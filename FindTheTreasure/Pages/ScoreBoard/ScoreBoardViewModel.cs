using FindTheTreasure.Pages.ScoreBoard.Models;
using FindTheTreasure.Services.Game;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FindTheTreasure.Pages.ScoreBoard
{
    public class ScoreBoardViewModel : INotifyPropertyChanged
    {
        private readonly GameService _gameService;
        private ICollection<Scoreboard> _scoreBoards;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICollection<Scoreboard> Scoreboards
        {
            get => _scoreBoards;
            set
            {
                _scoreBoards = value;
                OnPropertyChanged();;
            }
        }

        public void Refresh()
        {
            FillScoreBoards();
        }

        public ScoreBoardViewModel(GameService gameService)
        {
            _gameService = gameService;
            Scoreboards = new List<Scoreboard>();
            FillScoreBoards();
        }

        private async void FillScoreBoards()
        {
            Scoreboards = await _gameService.GetScoreBoards();
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
