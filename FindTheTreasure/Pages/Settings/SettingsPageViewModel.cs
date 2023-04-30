using FindTheTreasure.Services.Bluetooth;

namespace FindTheTreasure.Pages.Settings
{
    public partial class SettingsPageViewModel : BaseViewModel
    {
        public BluetoothPermissionsService BluetoothLEService { get; private set; }
        public SettingsPageViewModel(BluetoothPermissionsService bluetoothLEService)
        {
            Title = $"Settings";

            BluetoothLEService = bluetoothLEService;
        }
    }
}
