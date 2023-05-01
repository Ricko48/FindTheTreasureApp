using FindTheTreasure.Models;
using FindTheTreasure.Services.Bluetooth;
using FindTheTreasure.Services.Data;
using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;
using FindTheTreasure.Services.User;

namespace FindTheTreasure.Pages.Home
{
    public partial class HomeViewModel : BaseViewModel
    {
        public IAsyncRelayCommand ScanNearbyDevicesAsyncCommand { get; }
        public IAsyncRelayCommand CheckPermissionsAsyncCommand { get; }
        public IAsyncRelayCommand GoToBeaconDetailPageAsyncCommand { get; }

        private readonly BluetoothPermissionsService bluetoothPermissionsService;
        private readonly BeaconDiscoveryService beaconDiscoveryService;
        private readonly BeaconService beaconService;
        private readonly BeaconBluetoothDeviceMergeService beaconBluetoothDeviceMergeService;

        //CollectionView one-way binding from ViewModel to View
        public ObservableCollection<BeaconModel> DiscoveredDevices { get; set; } = new ObservableCollection<BeaconModel>();

        public HomeViewModel(
            BeaconBluetoothDeviceMergeService beaconBluetoothDeviceMergeService,
            BluetoothPermissionsService bluetoothPermissionsService,
            BeaconDiscoveryService beaconDiscoveryService,
            BeaconService beaconService,
            UserService userService)
        {
            Title = $"Scan and select device";

            this.beaconBluetoothDeviceMergeService = beaconBluetoothDeviceMergeService;
            this.beaconDiscoveryService = beaconDiscoveryService;
            this.beaconService = beaconService;
            this.bluetoothPermissionsService = bluetoothPermissionsService;

            ScanNearbyDevicesAsyncCommand = new AsyncRelayCommand(ScanDevicesAsync);
            CheckPermissionsAsyncCommand = new AsyncRelayCommand(CheckPermissionsAsync);
            GoToBeaconDetailPageAsyncCommand = new AsyncRelayCommand<BeaconModel>(GoToBeaconDetailPageAsync);

            if (!userService.IsLoggedIn())
            {
                Shell.Current.GoToAsync(nameof(SignInView), false).Wait();
            }
        }

        private async Task GoToBeaconDetailPageAsync(BeaconModel item)
        {
            Dictionary<string, object> parameters = new() { { nameof(BeaconDetailViewModel.Item), item } };
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

                var knownBeacons = beaconService.GetAll();
                //used to filter bluetooth devices when scanning
                var macAddresses = knownBeacons.Select(b => b.MAC).ToArray();

                await beaconDiscoveryService.StartScanning(macAddresses: macAddresses, maxItems: null);
                Debug.WriteLine("Finished scanning");
                var devices = beaconDiscoveryService.DiscoveredDevices;
                if (devices.Count == 0)
                {
                    await Shell.Current.DisplayAlert($"Info", $"No Bluetooth LE devices found", "OK");
                    return;
                }

                ShowDiscoveredDevices(beaconDiscoveryService.DiscoveredDevices);
            }
            catch (Exception ex)
            {
                IsScanning = false; //just to turn off spinner in UI before error popup shows, nothing else.
                Debug.WriteLine($"Unable to get nearby Bluetooth LE devices: {ex.Message}");
                await Shell.Current.DisplayAlert($"Unable to get nearby Bluetooth LE devices", $"{ex.Message}", "OK");
            }
            finally
            {
                IsScanning = false; //turns off spinner and turns on Scan button upon fail or success.
            }
        }

        private void ShowDiscoveredDevices(List<IDevice> discoveredBluetoothDevices)
        {
            var knownBeacons = beaconService.GetAll();
            var devicesToDisplay = beaconBluetoothDeviceMergeService.Merge(knownBeacons, discoveredBluetoothDevices);
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