using Android.Content;
using Android.Locations;
using FindTheTreasure.Pages.ScoreBoard;
using FindTheTreasure.Services.Bluetooth;
using FindTheTreasure.Services.Data;
using FindTheTreasure.Services.GPS;
using FindTheTreasure.Services.User;
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

        builder.Services.AddSingleton<HomeView>();
        builder.Services.AddSingleton<HomeViewModel>();

        builder.Services.AddSingleton<FoundBeaconsView>();
        builder.Services.AddSingleton<FoundBeaconsViewModel>();

        builder.Services.AddSingleton<BeaconDetailView>();
        builder.Services.AddSingleton<BeaconDetailViewModel>();

        builder.Services.AddSingleton<UserDetailView>();
        builder.Services.AddSingleton<UserDetailViewModel>();

        builder.Services.AddSingleton<SignInView>();
        builder.Services.AddSingleton<SignInViewModel>();

        builder.Services.AddSingleton<SignUpView>();
        builder.Services.AddSingleton<SignUpViewModel>();

        builder.Services.AddSingleton<ScoreBoardView>();
        builder.Services.AddSingleton<ScoreBoardViewModel>();

        builder.Services.AddSingleton<UserService>();

        return builder.Build();
    }
}
