using FindTheTreasure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FindTheTreasure.Pages.Game
{
    public class CreateGameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IAsyncRelayCommand CreateGame { get; }

        private GameModel gameModel;

        public GameModel GameModel
        {
            get => gameModel;
            set
            {
                gameModel = value;
                OnPropertyChanged_();
            }

        }


        public CreateGameViewModel()
        {
            CreateGame = new AsyncRelayCommand(CreateGameAsync);
        }

        protected virtual void OnPropertyChanged_([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Refresh()
        {
            gameModel.Id = -1;
            gameModel.Name = "";
        }

        private async Task CreateGameAsync()
        {
            //add game         
        }
    }
}
