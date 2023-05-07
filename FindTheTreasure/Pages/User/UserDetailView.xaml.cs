namespace FindTheTreasure.Pages.User;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class UserDetailView : ContentPage
{
    private readonly UserDetailViewModel _viewModel;

    public UserDetailView(UserDetailViewModel viewModel)
	{
        _viewModel = viewModel;
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        _viewModel.Refresh();
        base.OnAppearing();
    }
}