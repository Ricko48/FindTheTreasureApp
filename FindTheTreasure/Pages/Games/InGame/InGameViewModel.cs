using Refit;
using FindTheTreasure.Models;
using FindTheTreasure.Services.Beacons;
using FindTheTreasure.Services.Bluetooth;
using FindTheTreasure.Services.Game;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Net;

namespace FindTheTreasure.Pages.Games.InGame
{
    public partial class InGameViewModel : BaseViewModel
    {
        private readonly GameService _gameService;
        private readonly BluetoothPermissionsService bluetoothPermissionsService;
        private readonly BeaconDiscoveryService beaconDiscoveryService;
        private readonly BeaconsService beaconService;
        private readonly BeaconBluetoothDeviceMergeService beaconBluetoothDeviceMergeService;

        [ObservableProperty]
        private string puzzle; 
        private string _macAddress;

        public IAsyncRelayCommand ScanNearbyDevicesAsyncCommand { get; }
        public IAsyncRelayCommand CheckPermissionsAsyncCommand { get; }
        public ICommand StopGameButtonClickedCommand { get; }
        public ICommand ShowMapButtonClickedCommand { get; }
        public ICommand BeaconFoundCommand { get; }

        public ObservableCollection<BeaconModel> DiscoveredDevices { get; set; } = new();

        public InGameViewModel(
            GameService gameService,
            BluetoothPermissionsService bluetoothPermissionsService,
            BeaconDiscoveryService beaconDiscoveryService,
            BeaconsService beaconService,
            BeaconBluetoothDeviceMergeService beaconBluetoothDeviceMergeService)
        {
            this.beaconDiscoveryService = beaconDiscoveryService;
            this.beaconService = beaconService;
            this.bluetoothPermissionsService = bluetoothPermissionsService;
            _gameService = gameService;
            StopGameButtonClickedCommand = new Command(async () => await OnStopGameButtonClickedAsync());
            ShowMapButtonClickedCommand = new Command(async () => await OnShowMapButtonClickedAsync());
            ScanNearbyDevicesAsyncCommand = new AsyncRelayCommand(ScanDevicesAsync);
            CheckPermissionsAsyncCommand = new AsyncRelayCommand(CheckPermissionsAsync);
            BeaconFoundCommand = new Command(async () => await OnBeaconFoundAsync());

            InitializeMacAddress();
            this.beaconBluetoothDeviceMergeService = beaconBluetoothDeviceMergeService;
        }

        private async Task OnBeaconFoundAsync()
        {
            try
            {
                await Shell.Current.DisplayAlert("Success", "You found the beacon. Good luck with the next one!", "Ok");
                await beaconService.SetBeaconToFoundAsync(DiscoveredDevices.First().MacAddress);
                await SetNextBeacon();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong while connecting to the server.", "Ok");
            }
        }

        private void InitializeMacAddress()
        {
            SetNextBeacon().Wait();
        }

        private async Task SetNextBeacon()
        {
            try
            {
                var beacon = await beaconService.GetNextBeaconInGame();
                Puzzle = beacon.PositionDescription;
                _macAddress = beacon.MacAddress;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Congratulations", "All the beacons have been collected!", "Ok");
                await Shell.Current.Navigation.PopToRootAsync();
                _gameService.StopGame();
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
                    await Shell.Current.DisplayAlert("Bluetooth LE permissions", "Bluetooth LE permissions are not granted.", "OK");
                    return;
                }
            }
            await Shell.Current.DisplayAlert($"Permissions info", "Permissions OK", "OK");
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

                await beaconDiscoveryService.StartScanning(macAddresses: new[] { _macAddress }, maxItems: null);
                Debug.WriteLine("Finished scanning");
                var devices = beaconDiscoveryService.DiscoveredDevices;
                if (devices.Count == 0)
                {
                    await Shell.Current.DisplayAlert($"Info", $"No available devices found", "OK");
                    return;
                }

                var discovered = devices.Select(d => beaconBluetoothDeviceMergeService.Map(d)).ToList();
                DiscoveredDevices.Add(discovered.First());
                await OnBeaconFoundAsync();
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

        public async Task OnStopGameButtonClickedAsync()
        {
            _gameService.StopGame();
            await Shell.Current.Navigation.PopToRootAsync();
        }

        public async Task OnShowMapButtonClickedAsync()
        {
            await Shell.Current.GoToAsync(nameof(FoundBeaconsView), false);
        }
    }
}
