using FindTheTreasure.Services.Game;

namespace FindTheTreasure.Pages.Games.InGame;

public partial class InGameVIew : ContentPage
{
    private readonly GameService _gameService;
	public InGameVIew(InGameViewModel vieModel, GameService gameService)
	{
        _gameService = gameService;
        InitializeComponent();
        BindingContext = vieModel;
    }

    // to disable go back functionality
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}