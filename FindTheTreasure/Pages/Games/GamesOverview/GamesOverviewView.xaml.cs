using AndroidX.Lifecycle;

namespace FindTheTreasure.Pages.Games.GamesOverview;

public partial class GamesOverviewView : ContentPage
{
    private GamesOverviewViewModel ViewModel;
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.GetGames();
    }

    public GamesOverviewView(GamesOverviewViewModel viewModel)
	{
        ViewModel = viewModel;
		InitializeComponent();
        BindingContext = viewModel;
    }
}