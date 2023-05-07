using AndroidX.Lifecycle;

namespace FindTheTreasure.Pages.Games.ScanBeacons;

public partial class ScanGameBeaconsView : ContentPage
{
	public ScanGameBeaconsView(ScanGameBeaconsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}