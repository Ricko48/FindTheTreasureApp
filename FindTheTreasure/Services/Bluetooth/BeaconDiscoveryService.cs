using FindTheTreasure.Services.GPS;

using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Exceptions;

namespace FindTheTreasure.Services.Bluetooth
{
    /// <summary>
    /// Discovery of bluetooth beacons in the range of the device.
    /// <br>Does not provide functionality to communicate to these devices.</br>
    /// </summary>
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

        /// <summary>
        /// Devices discovered since last scan started.
        /// </summary>
        public List<IDevice> DiscoveredDevices => discoveredDevices.ToList(); //the list is copied to prevent modification

        /// <summary>
        /// Contains devices found since last scanning started.
        /// </summary>
        private readonly List<IDevice> discoveredDevices = new List<IDevice>();

        /// <summary>
        /// If the bluetooth adapter is currently scanning.
        /// <br>Starting multiple parallel scannings is a problem on Android (from the documentation)</br>
        /// </summary>
        public bool Scanning => adapter.IsScanning;

        //interval k nalezení by měl být dostatečně dlouhý

        //scan timeout on adapter can be adjusted during runtime.

        private int? currentScanningMaxItems = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="macAddresses">for filtering devices</param>
        /// <param name="timeoutMilliseconds">for cancelling scanning after a period had elapsed</param>
        /// <param name="cancellationToken">for cancelling scanning</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">If bluetooth is not turned on (and GPS on Android)</exception>
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

            discoveredDevices.Clear(); //reset to 0 items
            currentScanningMaxItems = maxItems;

            ScanFilterOptions options = new ScanFilterOptions()
            {
                DeviceAddresses = macAddresses,
            };

            /* Test for 2 BLE EMBC01 located 10 cm from the device with max signal strength setting and interval 1000 ms:
             * min 500, max 6000, 9000 ms, average 3000 ms */
            adapter.ScanTimeout = timeoutMilliseconds; //default is 10 000 in Plugin.BLE if none is provided 

            scanningStopwatch.Restart();
            await adapter.StartScanningForDevicesAsync(scanFilterOptions: options, cancellationToken: cancellationToken);
        }

        //measures time spent scanning before discovering a device
        private Stopwatch scanningStopwatch = new Stopwatch();

        public async Task StopScanning()
        {
            await adapter.StopScanningForDevicesAsync();
        }

        //note: to display something in the UI, this has to be used: MainThread.BeginInvokeOnMainThread(async () => { the code... }

        //POZOR: pokud není nalezené žádné zařízení, je potřeba zapnout GPS (nedokumentovaná "feature")!!!!!!!!!!!!!!
        //https://github.com/dotnet-bluetooth-le/dotnet-bluetooth-le/issues/321#issuecomment-430253961
        private async void Adapter_DeviceDiscovered(object sender, DeviceEventArgs e)
        {
            Debug.WriteLine($"Discovered device {scanningStopwatch.ElapsedMilliseconds} ms after scanning started");

            var device = e.Device;
            //u bluetooth zařízení na androidu je v poslední části GUIDu MAC adresa (a začátek GUIDu  jsou nuly)
            //např.: 00000000-0000-0000-0000-0cf3eeb8de3 8, Name: , Rssi: -34, MAC: 0C:F3:EE:B8:DE:38
            //u beaconů někdy funguje oznamování jména, jindy není oznámeno žádné (a nemění se to s aktuálním módem signálování (1/2/3))
            //spolehlivá je zde pouze MAC adresa
            string mac = null;

            mac = GetAndroidBluetoothDeviceMacAddress(device.NativeDevice as Android.Bluetooth.BluetoothDevice);

            Debug.WriteLine($"Discovered device: " +
                $"ID: {device.Id}, Name: {device.Name}, Rsii: {device.Rssi}, MAC address: {mac}"); //DEBUG
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
