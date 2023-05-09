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

 
        public List<BeaconModel> Merge(List<BeaconModel> knownBeacons, List<IDevice> discoveredBluetoothDevices)
        {
            var discovered = discoveredBluetoothDevices.Select(d => Map(d)).ToList();          
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

        public BeaconModel Map(IDevice device)
        {
            var mac = bluetoothDeviceMacAddressService.GetMacAddress(device);
            return new BeaconModel
            {
                MacAddress = mac,
                Name = "",
            };
        }
    }
}
