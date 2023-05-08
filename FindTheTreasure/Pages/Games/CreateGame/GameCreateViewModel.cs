using FindTheTreasure.Models;
using FindTheTreasure.Pages.Games.ScanBeacons;
using FindTheTreasure.Services.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Android.Content.ClipData;

namespace FindTheTreasure.Pages.Games.CreateGame
{
    public class GameCreateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IAsyncRelayCommand CreateGame { get; }

        private GameModel gameModel;

        private GameService GameService;

        public GameModel GameModel
        {
            get => gameModel;
            set
            {
                gameModel = value;
                OnPropertyChanged_();
            }

        }

        public GameCreateViewModel(GameService gameService)
        {
            CreateGame = new AsyncRelayCommand(CreateGameAsync);
            GameService = gameService;
        }

        protected virtual void OnPropertyChanged_([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Refresh()
        {
            GameModel = new GameModel();
        }

        private async Task CreateGameAsync()
        {
            //get real wonerID
            gameModel.OwnerId = 1;
            var createGame = new CreateGameModel
            {
                Name = gameModel.Name,
                OwnerId = gameModel.OwnerId,
            };
            gameModel.Id = await GameService.CreateGameAsync(createGame);
            Dictionary<string, object> parameters = new() { { nameof(ScanGameBeaconsViewModel.Item), gameModel } };
            await Shell.Current.GoToAsync(nameof(ScanGameBeaconsView), true, parameters);
        }
    }
}
