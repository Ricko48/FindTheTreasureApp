using FindTheTreasure.Models;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Exceptions;
using Plugin.BLE.Abstractions.Contracts;
using FindTheTreasure.Services.Bluetooth;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static Android.Content.ClipData;

namespace FindTheTreasure.Pages.Beacon
{
    [QueryProperty(nameof(Item), nameof(Item))]
    public partial class AddBeaconToGameViewModel : INotifyPropertyChanged
    {
        public IAsyncRelayCommand AddDeviceToGameAsyncCommand { get; }
        private IAdapter adapter;
        private BeaconDiscoveryService beaconDiscoveryService;

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

        public AddBeaconToGameViewModel(IAdapter adapter, BeaconDiscoveryService beaconDiscoveryService)
        {
            AddDeviceToGameAsyncCommand = new AsyncRelayCommand(AddDeviceToGameAsync);
            this.adapter = adapter;
            this.beaconDiscoveryService = beaconDiscoveryService;                     
        }

        protected virtual void OnPropertyChanged_([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Refresh()
        {
            GameBeacon.Puzze = "";
            GameBeacon.Name = "";
            GameBeacon.GameID = -1;
        }

        private async Task AddDeviceToGameAsync()
        {
            //add to game
            string myText = GameBeacon.Name;
            var y = 1;
        }

    }
}
