namespace FindTheTreasure.Pages.FoundBeacons;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FoundBeaconsView : ContentPage
{
    private readonly FoundBeaconsViewModel _viewModel;

    public FoundBeaconsView(FoundBeaconsViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
        Map.IsShowingUser = true;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Refresh(Map);
    }
}