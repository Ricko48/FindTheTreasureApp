using Plugin.BLE.Abstractions.Contracts;

namespace FindTheTreasure.Services.Bluetooth
{
    public class BluetoothDeviceMacAddressService
    {
        /// <summary>
        /// Implemented only for Android platform
        /// </summary>
        public string GetMacAddress(IDevice bluetoothDevice)
        {
#if ANDROID
            if (bluetoothDevice.NativeDevice is Android.Bluetooth.BluetoothDevice device)
            {
                return device.Address;
            }
#endif
            return null;
        }
    }
}
