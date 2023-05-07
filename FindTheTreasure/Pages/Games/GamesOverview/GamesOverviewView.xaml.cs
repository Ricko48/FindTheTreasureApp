using AndroidX.Lifecycle;

namespace FindTheTreasure.Pages.Games.GamesOverview;

public partial class GamesOverviewView : ContentPage
{
	public GamesOverviewView(GamesOverviewViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}