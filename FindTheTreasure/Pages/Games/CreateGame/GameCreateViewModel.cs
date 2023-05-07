﻿using FindTheTreasure.Models;
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
            gameModel.OwnerId = 1;
            var g = gameModel;
            var createGame = new CreateGameModel
            {
                Name = gameModel.Name,
                OwnerId = gameModel.OwnerId,
                Owner = new Models.User
                {
                    Id = 1,
                    FirstName = "xx",
                    LastName = "xx",
                    UserName = ""
                },
                GameBeacons = new List<GameBeacon>(),
                GameParticipants = new List<GameParticipant>(),
            };
            gameModel.Id = await GameService.CreateGameAsync(createGame); 
            
            /*Dictionary<string, object> parameters = new() { { nameof(ScanGameBeaconsViewModel.GameModel), gameModel } };
            await Shell.Current.GoToAsync(nameof(ScanGameBeaconsView), true, parameters);*/
        }
    }
}