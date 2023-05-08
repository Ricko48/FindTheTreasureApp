namespace FindTheTreasure.Pages.Games.InGame;

public partial class InGameVIew : ContentPage
{
	public InGameVIew(InGameViewModel vieModel)
	{
        InitializeComponent();
        BindingContext = vieModel;
    }

    // to disable go back functionality
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}