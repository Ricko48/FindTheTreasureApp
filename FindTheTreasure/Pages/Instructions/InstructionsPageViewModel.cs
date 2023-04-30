using FindTheTreasure.Services.Bluetooth;

namespace FindTheTreasure.Pages.Instructions;

public partial class InstructionsPageViewModel : BaseViewModel
{
    public BluetoothPermissionsService BluetoothLEService { get; private set; }
    public InstructionsPageViewModel(BluetoothPermissionsService bluetoothLEService)
    {
        Title = $"Instructions";

        BluetoothLEService = bluetoothLEService;
    }
}

