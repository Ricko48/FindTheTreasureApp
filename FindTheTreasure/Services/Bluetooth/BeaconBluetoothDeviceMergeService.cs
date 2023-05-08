using FindTheTreasure.Models;

using Plugin.BLE.Abstractions.Contracts;

namespace FindTheTreasure.Services.Bluetooth
{
    public class BeaconBluetoothDeviceMergeService
    {
        private readonly BluetoothDeviceMacAddressService bluetoothDeviceMacAddressService;

        public BeaconBluetoothDeviceMergeService(BluetoothDeviceMacAddressService bluetoothDeviceMacAddressService)
        {
            this.bluetoothDeviceMacAddressService = bluetoothDeviceMacAddressService;
        }

        /// <summary>
        /// Adds info from known beacons to discovered beacons for these which match by MAC address.
        /// <br>If there is no match by MAC address, the discovered beacon will be returned without info.</br>
        /// </summary>
        /// <param name="knownBeacons"></param>
        /// <param name="discoveredBluetoothDevices"></param>
        public List<BeaconModel> Merge(List<BeaconModel> knownBeacons, List<IDevice> discoveredBluetoothDevices)
        {
            var discovered = (from d in discoveredBluetoothDevices select Map(d)).ToList();
            var discoveredWithInfos = new List<BeaconModel>();
            foreach(var beacon in discovered)
            {
                var discoveredWithInfo = (from known in knownBeacons where known.MacAddress == beacon.MacAddress select known).SingleOrDefault();
                if(discoveredWithInfo is not null)
                {
                    discoveredWithInfos.Add(discoveredWithInfo);
                }
            }
            return discoveredWithInfos;
        }

        private BeaconModel Map(IDevice device)
        {
            var mac = bluetoothDeviceMacAddressService.GetMacAddress(device);
            return new BeaconModel
            {
                MacAddress = mac,
                Name = "unknown",
                Number = null,
                SerialNumber = null
            };
        }
    }
}
