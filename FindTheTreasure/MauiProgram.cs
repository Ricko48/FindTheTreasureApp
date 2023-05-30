using System.Reflection;
using Android.Content;
using Android.Locations;
using FindTheTreasure.Services.Beacons;
using FindTheTreasure.Services.Beacons.API;
using FindTheTreasure.Services.Bluetooth;
using FindTheTreasure.Services.Game;
using FindTheTreasure.Services.Game.API;
using FindTheTreasure.Services.GPS;
using FindTheTreasure.Services.User;
using FindTheTreasure.Services.User.API;
using Microsoft.Extensions.Configuration;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Refit;

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

        builder.Services.AddSingleton<IBluetoothLE>(CrossBluetoothLE.Current);
        builder.Services.AddSingleton<IAdapter>(CrossBluetoothLE.Current.Adapter);

        builder.Services.AddSingleton<BluetoothPermissionsService>();
        builder.Services.AddSingleton<BeaconDiscoveryService>();
        builder.Services.AddSingleton<BluetoothDeviceMacAddressService>();

        builder.Services.AddSingleton<LocationManager>((LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService));

        builder.Services.AddSingleton<AndroidGPSFeatureService>();

        builder.Services.AddSingleton<BeaconsService>();
        builder.Services.AddSingleton<BeaconBluetoothDeviceMergeService>();

        builder.Services.AddSingleton<FoundBeaconsView>();
        builder.Services.AddSingleton<FoundBeaconsViewModel>();

        builder.Services.AddSingleton<BeaconDetailView>();
        builder.Services.AddSingleton<AddBeaconToGameViewModel>();

        builder.Services.AddSingleton<UserDetailView>();
        builder.Services.AddSingleton<UserDetailViewModel>();

        builder.Services.AddSingleton<SignInView>();
        builder.Services.AddSingleton<SignInViewModel>();

        builder.Services.AddSingleton<SignUpView>();
        builder.Services.AddSingleton<SignUpViewModel>();

        builder.Services.AddSingleton<ScoreBoardView>();
        builder.Services.AddSingleton<ScoreBoardViewModel>();

        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<GameService>();

        builder.Services.AddSingleton<GamesOverviewViewModel>();
        builder.Services.AddSingleton<GamesOverviewView>();

        builder.Services.AddSingleton<GameCreateView>();
        builder.Services.AddSingleton<GameCreateViewModel>();

        builder.Services.AddSingleton<ScanGameBeaconsView>();
        builder.Services.AddSingleton<ScanGameBeaconsViewModel>();

        builder.Services.AddSingleton<InGameVIew>();
        builder.Services.AddSingleton<InGameViewModel>();

        const string apiUrl = "http://localhost:80";

        // register api clients
        builder.Services.AddSingleton(RestService.For<IUserApiClient>(apiUrl));
        builder.Services.AddSingleton(RestService.For<IBeaconsApiClient>(apiUrl));
        builder.Services.AddSingleton(RestService.For<IGameApiClient>(apiUrl));

        return builder.Build();
    }
}
