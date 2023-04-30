using FindTheTreasure.Services.Bluetooth;

namespace FindTheTreasure.Pages.Privacy
{
    public partial class PrivacyStatementPageViewModel : BaseViewModel
    {
        public BluetoothPermissionsService BluetoothLEService { get; private set; }
        public PrivacyStatementPageViewModel(BluetoothPermissionsService bluetoothLEService)
        {
            Title = $"Privacy statement";

            BluetoothLEService = bluetoothLEService;
        }
    }
}
