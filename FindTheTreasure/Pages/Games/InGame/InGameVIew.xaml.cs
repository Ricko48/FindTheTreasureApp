namespace FindTheTreasure.Pages.Games.InGame;

public partial class InGameVIew : ContentPage
{
	public InGameVIew(InGameViewModel vieModel)
	{
		BindingContext = vieModel;
		InitializeComponent();
	}
}