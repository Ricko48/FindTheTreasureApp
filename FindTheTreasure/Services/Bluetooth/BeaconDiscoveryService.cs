using FindTheTreasure.Services.GPS;

using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;

namespace FindTheTreasure.Services.Bluetooth
{
    public class BeaconDiscoveryService
    {
        private readonly IBluetoothLE bluetoothLE;
        public IAdapter adapter;
        private readonly AndroidGPSFeatureService gpsFeatureService;

        public BeaconDiscoveryService(IBluetoothLE bluetoothLE, IAdapter adapter, AndroidGPSFeatureService gpsFeatureService)
        {
            this.bluetoothLE = bluetoothLE;
            this.adapter = adapter;
            this.gpsFeatureService = gpsFeatureService;

            adapter.DeviceDiscovered += Adapter_DeviceDiscovered;
        }

        public async Task ConnectToDeviceCandidateAsync()
        {
            var device = DiscoveredDevices[0];
            var s  = device.Id;
            var y = 0;
            try { await adapter.ConnectToDeviceAsync((IDevice)DiscoveredDevices[0]); }
            catch (DeviceConnectionException e)
            {
                Debug.WriteLine(e.ToString());
                await Shell.Current.DisplayAlert($"Info", e.Message, "Ok");
                
            }           
        }


        public List<IDevice> DiscoveredDevices => discoveredDevices.ToList(); 

        private readonly List<IDevice> discoveredDevices = new List<IDevice>();

        public bool Scanning => adapter.IsScanning;

        private int? currentScanningMaxItems = null;

        public async Task StartScanning(string[] macAddresses = null, int timeoutMilliseconds = 10000, int? maxItems = null, CancellationToken cancellationToken = default)
        {
            if (bluetoothLE.IsOn == false)
            {
                throw new InvalidOperationException($"Bluetooth is not turned on. Please turn on bluetooth.");
            }

            if(gpsFeatureService.IsGPSOn() == false)
            {
                throw new InvalidOperationException($"GPS is not turned on. Please turn on GPS to be able to scan for bluetooth low-energy devices.");
            }

            discoveredDevices.Clear();
            currentScanningMaxItems = maxItems;

            ScanFilterOptions options = new ScanFilterOptions()
            {
                DeviceAddresses = macAddresses,
            };
      
            adapter.ScanTimeout = timeoutMilliseconds; 

            scanningStopwatch.Restart();
            await adapter.StartScanningForDevicesAsync(scanFilterOptions: options, cancellationToken: cancellationToken);
        }

        private Stopwatch scanningStopwatch = new Stopwatch();

        public async Task StopScanning()
        {
            await adapter.StopScanningForDevicesAsync();
        }

        private async void Adapter_DeviceDiscovered(object sender, DeviceEventArgs e)
        {
            Debug.WriteLine($"Discovered device {scanningStopwatch.ElapsedMilliseconds} ms after scanning started");

            var device = e.Device;
            string mac = null;

            mac = GetAndroidBluetoothDeviceMacAddress(device.NativeDevice as Android.Bluetooth.BluetoothDevice);

            Debug.WriteLine($"Discovered device: " +
                $"ID: {device.Id}, Name: {device.Name}, Rsii: {device.Rssi}, MAC address: {mac}");
            discoveredDevices.Add(device);

            if(currentScanningMaxItems != null && currentScanningMaxItems >= discoveredDevices.Count)
            {
                await StopScanning();
            }
        }

        private string GetAndroidBluetoothDeviceMacAddress(Android.Bluetooth.BluetoothDevice device)
        {
            if (device is null)
            {
                return null;
            }
            return device.Address;
        }
    }
}
