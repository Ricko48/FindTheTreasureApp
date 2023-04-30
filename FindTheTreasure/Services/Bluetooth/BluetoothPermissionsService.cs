namespace FindTheTreasure.Services.Bluetooth;

public class BluetoothPermissionsService
{
#if ANDROID
    public async Task<PermissionStatus> CheckBluetoothPermissions()
    {
        PermissionStatus status = PermissionStatus.Unknown;
        try
        {
            status = await Permissions.CheckStatusAsync<BluetoothLEPermissions>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to check Bluetooth LE permissions: {ex.Message}.");
            await Shell.Current.DisplayAlert($"Unable to check Bluetooth LE permissions", $"{ex.Message}.", "OK");
        }
        return status;
    }

    public async Task<PermissionStatus> RequestBluetoothPermissions()
    {
        PermissionStatus status = PermissionStatus.Unknown;
        try
        {
            status = await Permissions.RequestAsync<BluetoothLEPermissions>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to request Bluetooth LE permissions: {ex.Message}.");
        }
        return status;
    }
#elif IOS
#elif WINDOWS
#endif
}

