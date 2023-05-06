namespace FindTheTreasure.Pages.Game;

public partial class CreateGameView : ContentPage
{
    private CreateGameViewModel ViewModel;
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.Refresh();
    }

    public CreateGameView(CreateGameViewModel viewModel = null)
    {
        ViewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;

    }
}