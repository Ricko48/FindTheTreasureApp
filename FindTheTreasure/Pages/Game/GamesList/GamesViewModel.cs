using FindTheTreasure.Models;
using FindTheTreasure.Services.Bluetooth;
using FindTheTreasure.Services.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Graphics.ColorSpace;

namespace FindTheTreasure.Pages.Game.GamesList
{
    public class GamesViewModel : ObservableObject
    {       
        public IAsyncRelayCommand GoCreateGameAsyncCommand { get; }
        public IAsyncRelayCommand DeleteGameAsyncCommand { get; }
        public IAsyncRelayCommand GoToStartGamePageAsyncCommand { get; }

        public ObservableCollection<GameModel> Games { get; set; } = new ObservableCollection<GameModel>();
        public GamesViewModel(UserService userService)
        {
            GoCreateGameAsyncCommand = new AsyncRelayCommand(GoToCreateGameAsync);
            DeleteGameAsyncCommand = new AsyncRelayCommand(DeleteGameAsync);
            GoToStartGamePageAsyncCommand = new AsyncRelayCommand<BeaconModel>(GoToStartGamePageAsync);

            if (!userService.IsSignedIn())
            {
                Shell.Current.GoToAsync(nameof(SignInView), false).Wait();
            }

            GetGames();
        }

        private void GetGames()
        {
            //return games
        }

        private async Task GoToStartGamePageAsync(BeaconModel item)
        {
            //Dictionary<string, object> parameters = new() { { nameof(AddBeaconToGameViewModel.Item), item } };
            //await Shell.Current.GoToAsync(nameof(BeaconDetailView), true, parameters);
        }

        private async Task GoToCreateGameAsync()
        {
            await Shell.Current.GoToAsync(nameof(CreateGameView), true);
        }

        private async Task DeleteGameAsync()
        {
            
        }
    }
}
