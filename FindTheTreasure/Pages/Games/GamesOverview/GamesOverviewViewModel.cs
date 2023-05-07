﻿using FindTheTreasure.Models;
using FindTheTreasure.Pages.Games.CreateGame;
using FindTheTreasure.Services.Game;
using FindTheTreasure.Services.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheTreasure.Pages.Games.GamesOverview
{
    public class GamesOverviewViewModel : ObservableObject
    {       
        public IAsyncRelayCommand GoCreateGameAsyncCommand { get; }
        public IAsyncRelayCommand DeleteGameAsyncCommand { get; }
        public IAsyncRelayCommand GoToStartGamePageAsyncCommand { get; }
        private GameService GameService;

        public ObservableCollection<GameModel> Games { get; set; } = new ObservableCollection<GameModel>();
        public GamesOverviewViewModel(UserService userService, GameService gameService)
        {           
            GameService = gameService;
            GoCreateGameAsyncCommand = new AsyncRelayCommand(GoToCreateGameAsync);
            DeleteGameAsyncCommand = new AsyncRelayCommand(DeleteGameAsync);
            GoToStartGamePageAsyncCommand = new AsyncRelayCommand<BeaconModel>(GoToStartGamePageAsync);

            if (!userService.IsSignedIn())
            {
                Shell.Current.GoToAsync(nameof(SignInView), false).Wait();
            }

            //GetGames();
            
        }

        private async void GetGames()
        {
            /*var games = await GameService.GetGamesAsync();
            games.ForEach(g => Games.Add(g));*/
        }

        private async Task GoToStartGamePageAsync(BeaconModel item)
        {
            //Dictionary<string, object> parameters = new() { { nameof(AddBeaconToGameViewModel.Item), item } };
            //await Shell.Current.GoToAsync(nameof(BeaconDetailView), true, parameters);
        }

        private async Task GoToCreateGameAsync()
        {
            await Shell.Current.GoToAsync(nameof(GameCreateView), true);
        }

        private async Task DeleteGameAsync()
        {

        }
    }
}