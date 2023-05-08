using FindTheTreasure.Models;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Exceptions;
using Plugin.BLE.Abstractions.Contracts;
using FindTheTreasure.Services.Bluetooth;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FindTheTreasure.Services.Game;
using FindTheTreasure.Services.Beacons;
using FindTheTreasure.Services.GPS;

namespace FindTheTreasure.Pages.Beacon
{
    [QueryProperty(nameof(Item), nameof(Item))]
    public partial class AddBeaconToGameViewModel : INotifyPropertyChanged
    {
        public IAsyncRelayCommand AddDeviceToGameAsyncCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public BeaconModel Item { get; set; }

        private AndroidGPSFeatureService AndroidGPSService;

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
        private BeaconsService BeaconsService;

        public AddBeaconToGameViewModel(GameService gameService, BeaconsService beaconsService, AndroidGPSFeatureService androidGPSService)
        {
            AddDeviceToGameAsyncCommand = new AsyncRelayCommand(AddDeviceToGameAsync);
            GameService = gameService;
            BeaconsService = beaconsService;
            AndroidGPSService = androidGPSService;
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
            var location = await AndroidGPSService.GetCurrentLocation();
            var loc = await AndroidGPSService.GetCachedLocation();
            var beacon = new UpdateBeaconModel
            {
                GameId = Item.GameID,
                Id = Item.Id,
                MacAddress = Item.MacAddress,
                PositionDescription = "",
                Name = Item.Name,
                Puzzle = Item.Puzzle,

            };
            await BeaconsService.UpdateBeacon(beacon);
            bool su = await GameService.AddBeaconToGameAsync(Item.GameID, Item.Id); 
            if (su)
            {
                await Shell.Current.DisplayAlert("Alert", "Beacon added", "Ok");
            }
        }

    }
}
