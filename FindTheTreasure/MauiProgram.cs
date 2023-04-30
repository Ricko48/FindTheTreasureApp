using Android.Content;
using Android.Locations;
using FindTheTreasure.Pages.ScoreBoard;
using FindTheTreasure.Services.Bluetooth;
using FindTheTreasure.Services.Data;
using FindTheTreasure.Services.GPS;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace FindTheTreasure;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>();
        builder.UseMauiCommunityToolkit();

        builder.ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa_solid.ttf", "FontAwesome");
                fonts.AddFont("fa-brands-400.ttf", "FontAwesomeBrands");
            });

        builder.UseMauiMaps();

        builder.Services.AddSingleton<BluetoothPermissionsService>();

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        //builder.Services.AddSingleton<IMap>(Map.Default);

        //register Bluetooth Low-Energy library (Plugin.BLE)
        builder.Services.AddSingleton<IBluetoothLE>(CrossBluetoothLE.Current);
        builder.Services.AddSingleton<IAdapter>(CrossBluetoothLE.Current.Adapter);

        //custom Bluetooth services (using Plugin.BLE)
        builder.Services.AddSingleton<BluetoothPermissionsService>();
        builder.Services.AddSingleton<BeaconDiscoveryService>();
        builder.Services.AddSingleton<BluetoothDeviceMacAddressService>();

        builder.Services.AddSingleton<LocationManager>((LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService));

        builder.Services.AddSingleton<AndroidGPSFeatureService>();

        builder.Services.AddSingleton<BeaconService>();
        builder.Services.AddSingleton<BeaconBluetoothDeviceMergeService>();

        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<HomePageViewModel>();

        builder.Services.AddSingleton<FoundBeaconsPage>();
        builder.Services.AddSingleton<FoundBeaconsPageModel>();

        builder.Services.AddSingleton<BeaconDetailPage>();
        builder.Services.AddSingleton<BeaconDetailPageViewModel>();

        builder.Services.AddSingleton<AccountDetailPage>();
        builder.Services.AddSingleton<AccountDetailPageModel>();

        builder.Services.AddSingleton<ScoreBoardPage>();
        builder.Services.AddSingleton<ScoreBoardPageModel>();

        return builder.Build();
    }
}
