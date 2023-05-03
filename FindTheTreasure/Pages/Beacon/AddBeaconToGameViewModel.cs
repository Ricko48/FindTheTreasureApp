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
        public IAsyncRelayCommand GoBackAsyncCommand { get; }
        private IAdapter adapter;
        private BeaconDiscoveryService beaconDiscoveryService;

        public event PropertyChangedEventHandler PropertyChanged;    

        
        public BeaconModel Item { get; set; }

        private string clue;

        public string Clue
        {
            get => clue;
            set 
            {
                clue = value;
                OnPropertyChanged_();
            } 

        }

        public AddBeaconToGameViewModel(IAdapter adapter, BeaconDiscoveryService beaconDiscoveryService)
        {
            AddDeviceToGameAsyncCommand = new AsyncRelayCommand(AddDeviceToGameAsync);
            GoBackAsyncCommand = new AsyncRelayCommand(GoBack);
            this.adapter = adapter;
            this.beaconDiscoveryService = beaconDiscoveryService;                     
        }

        protected virtual void OnPropertyChanged_([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Refresh()
        {
            Clue = "";           
        }

        private async Task AddDeviceToGameAsync()
        {
            string myText = clue;
            var y = 1;
        }

        private async Task GoBack()
        {

        }
    }
}
