using FindTheTreasure.Models;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Exceptions;
using Plugin.BLE.Abstractions.Contracts;
using FindTheTreasure.Services.Bluetooth;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static Android.Content.ClipData;
using FindTheTreasure.Services.Game;

namespace FindTheTreasure.Pages.Beacon
{
    [QueryProperty(nameof(Item), nameof(Item))]
    public partial class AddBeaconToGameViewModel : INotifyPropertyChanged
    {
        public IAsyncRelayCommand AddDeviceToGameAsyncCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;    

        
        public BeaconModel Item { get; set; }


        private GameBeacon gameBeacon;

        public GameBeacon GameBeacon
        {
            get => gameBeacon;
            set 
            {
                gameBeacon = value;
                OnPropertyChanged_();
            } 

        }

        private GameService GameService;

        public AddBeaconToGameViewModel( GameService gameService)
        {
            AddDeviceToGameAsyncCommand = new AsyncRelayCommand(AddDeviceToGameAsync);
            GameService = gameService;
        }

        protected virtual void OnPropertyChanged_([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Refresh()
        {
            GameBeacon = new GameBeacon();
        }

        private async Task AddDeviceToGameAsync()
        {
            bool su = await GameService.AddBeaconToGameAsync(Item.GameID, Item.Id); 
            if (su)
            {
                await Shell.Current.DisplayAlert("Alert", "Beacon added", "Ok");
            }
        }

    }
}
