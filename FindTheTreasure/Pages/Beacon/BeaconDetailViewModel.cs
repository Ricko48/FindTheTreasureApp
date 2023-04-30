using FindTheTreasure.Models;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Exceptions;
using Plugin.BLE.Abstractions.Contracts;
using FindTheTreasure.Services.Bluetooth;

namespace FindTheTreasure.Pages.Beacon
{
    [QueryProperty(nameof(Item), nameof(Item))] //binding of query parameter to automatically generated public property by MVVM toolkit
    public partial class BeaconDetailViewModel : BaseViewModel
    {
        public IAsyncRelayCommand ConnectToDeviceCandidateAsyncCommand { get; }
        public IAsyncRelayCommand DisconnectFromDeviceAsyncCommand { get; }
        private IAdapter adapter;
        private BeaconDiscoveryService beaconDiscoveryService;

        [ObservableProperty] //MVVM toolkit creates public property for binding through View
        private BeaconModel item;

        public BeaconDetailViewModel(IAdapter adapter, BeaconDiscoveryService beaconDiscoveryService)
        {
            Title = $"Heart rate";

            ConnectToDeviceCandidateAsyncCommand = new AsyncRelayCommand(ConnectToDeviceCandidateAsync);
            DisconnectFromDeviceAsyncCommand = new AsyncRelayCommand(DisconnectFromDeviceAsync);
            this.adapter = adapter;
            this.beaconDiscoveryService = beaconDiscoveryService;
        }

        [ObservableProperty]
        ushort heartRateValue;

        [ObservableProperty]
        DateTimeOffset timestamp;

        private async Task ConnectToDeviceCandidateAsync()
        {
            await beaconDiscoveryService.adapter.ConnectToDeviceAsync((IDevice)beaconDiscoveryService.DiscoveredDevices[0]);
            var yyx = 0;
            await beaconDiscoveryService.ConnectToDeviceCandidateAsync();

            var yy = 0;
            //adapter = CrossBluetoothLE.Current.Adapter;
            string guid = "00000000-0000-0000-0000-";
            string g = "00000000-0000-0000-0000-0cf3eeb8dd0a";
            guid += item.MAC;
            guid = guid.Replace(":", "");
            var id = beaconDiscoveryService.DiscoveredDevices[0].Id;
            var y = 0;
            
            await adapter.ConnectToDeviceAsync((IDevice)beaconDiscoveryService.DiscoveredDevices[0]);
            //await adapter.ConnectToKnownDeviceAsync(beaconDiscoveryService.DiscoveredDevices[0].Id, cancellationToken: new CancellationToken());
            /*try
            {
                await adapter.ConnectToKnownDeviceAsync(deviceGuid: Guid.Parse(guid));
            }
            catch (DeviceConnectionException e)
            {
                await Shell.Current.DisplayAlert($"Info", $"Coud not connect to device", "OK");
            }*/
            var x = 0;
            await Shell.Current.DisplayAlert($"Info", $"Connected", "OK");
        }

        private async Task DisconnectFromDeviceAsync()
        {

        }
    }
}
