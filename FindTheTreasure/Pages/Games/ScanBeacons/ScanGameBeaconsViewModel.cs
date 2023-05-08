using FindTheTreasure.Models;
using FindTheTreasure.Services.Beacons;
using FindTheTreasure.Services.Bluetooth;
using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;

namespace FindTheTreasure.Pages.Games.ScanBeacons
{
    [QueryProperty(nameof(Item), nameof(Item))]
    public partial class ScanGameBeaconsViewModel : BaseViewModel
    {
        public GameModel Item { get; set; }
        public IAsyncRelayCommand ScanNearbyDevicesAsyncCommand { get; }
        public IAsyncRelayCommand CheckPermissionsAsyncCommand { get; }
        public IAsyncRelayCommand GoToBeaconDetailPageAsyncCommand { get; }

        private readonly BluetoothPermissionsService bluetoothPermissionsService;
        private readonly BeaconDiscoveryService beaconDiscoveryService;
        private readonly BeaconsService beaconService;
        private readonly BeaconBluetoothDeviceMergeService beaconBluetoothDeviceMergeService;

        public ObservableCollection<BeaconModel> DiscoveredDevices { get; set; } = new ObservableCollection<BeaconModel>();

        public ScanGameBeaconsViewModel(BeaconBluetoothDeviceMergeService beaconBluetoothDeviceMergeService,
            BluetoothPermissionsService bluetoothPermissionsService,
            BeaconDiscoveryService beaconDiscoveryService,
            BeaconsService beaconService)
        {
            this.beaconBluetoothDeviceMergeService = beaconBluetoothDeviceMergeService;
            this.beaconDiscoveryService = beaconDiscoveryService;
            this.beaconService = beaconService;
            this.bluetoothPermissionsService = bluetoothPermissionsService;

            ScanNearbyDevicesAsyncCommand = new AsyncRelayCommand(ScanDevicesAsync);
            CheckPermissionsAsyncCommand = new AsyncRelayCommand(CheckPermissionsAsync);
            GoToBeaconDetailPageAsyncCommand = new AsyncRelayCommand<BeaconModel>(GoToBeaconDetailPageAsync);
        }

        private async Task GoToBeaconDetailPageAsync(BeaconModel model)
        {
            model.GameID = Item.Id;
            //get real id
            var y = model.Id;
            model.Id = 1;
            Dictionary<string, object> parameters = new() { { nameof(AddBeaconToGameViewModel.Item), model } };
            await Shell.Current.GoToAsync(nameof(BeaconDetailView), true, parameters);
        }

        private async Task ScanDevicesAsync()
        {
            if (IsScanning)
            {
                return;
            }

            try
            {
                IsScanning = true;
                DiscoveredDevices.Clear();

                var knownBeacons = await beaconService.GetAllAsync();
                var macAddresses = knownBeacons.Select(b => b.MacAddress).ToArray();

                await beaconDiscoveryService.StartScanning(macAddresses: macAddresses, maxItems: null);
                Debug.WriteLine("Finished scanning");
                var devices = beaconDiscoveryService.DiscoveredDevices;
                if (devices.Count == 0)
                {
                    await Shell.Current.DisplayAlert($"Info", $"No available devices found", "OK");
                    return;
                }

                await ShowDiscoveredDevices(beaconDiscoveryService.DiscoveredDevices);
            }
            catch (Exception ex)
            {
                IsScanning = false;
                Debug.WriteLine($"Unable to get nearby Bluetooth  devices: {ex.Message}");
                await Shell.Current.DisplayAlert($"Unable to get nearby Bluetooth devices", $"{ex.Message}", "OK");
            }
            finally
            {
                IsScanning = false;
            }
        }

        private async Task ShowDiscoveredDevices(List<IDevice> discoveredBluetoothDevices)
        {
            var knownBeacons = await beaconService.GetAllAsync();
            var devicesToDisplay = beaconBluetoothDeviceMergeService.Merge(knownBeacons.ToList(), discoveredBluetoothDevices);
            DiscoveredDevices.Clear();
            foreach (var device in devicesToDisplay)
            {
                DiscoveredDevices.Add(device);
            }
        }

        private async Task CheckPermissionsAsync()
        {
            PermissionStatus permissionStatus = await bluetoothPermissionsService.CheckBluetoothPermissions();
            if (permissionStatus != PermissionStatus.Granted)
            {
                permissionStatus = await bluetoothPermissionsService.RequestBluetoothPermissions();
                if (permissionStatus != PermissionStatus.Granted)
                {
                    await Shell.Current.DisplayAlert($"Bluetooth LE permissions", $"Bluetooth LE permissions are not granted.", "OK");
                    return;
                }
            }
            await Shell.Current.DisplayAlert($"Permissions info", "Permissions OK", "OK");
        }


    }
}
