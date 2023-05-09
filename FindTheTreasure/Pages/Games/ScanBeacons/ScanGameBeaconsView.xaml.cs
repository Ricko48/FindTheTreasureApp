namespace FindTheTreasure.Pages.Games.ScanBeacons;

public partial class ScanGameBeaconsView : ContentPage
{
    private readonly ScanGameBeaconsViewModel _viewModel;

    public ScanGameBeaconsView(ScanGameBeaconsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    protected override void OnAppearing()
    {
        _viewModel.Refresh();
    }
}