using AndroidX.Lifecycle;

namespace FindTheTreasure.Pages.Games.CreateGame;

public partial class GameCreateView : ContentPage
{
    private GameCreateViewModel ViewModel;
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.Refresh();
    }

    public GameCreateView(GameCreateViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}