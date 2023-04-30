using Microsoft.Maui.Maps;

namespace FindTheTreasure.Pages.Map;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FoundBeaconsPage : ContentPage
{
    public FoundBeaconsPage(FoundBeaconsPageModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        SetMapToCurrentLocation();
    }

    private void SetMapToCurrentLocation()
    {
        var location = Geolocation.Default.GetLastKnownLocationAsync().Result;
        var currentLocationRegion = new MapSpan(location, 0.01, 0.01);
        map.MoveToRegion(currentLocationRegion);
        map.IsShowingUser = true;
    }
}