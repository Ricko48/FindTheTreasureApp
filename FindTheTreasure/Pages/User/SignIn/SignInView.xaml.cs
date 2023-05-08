namespace FindTheTreasure.Pages.User.SignIn;

public partial class SignInView : ContentPage
{
    private readonly SignInViewModel _viewModel;
    public SignInView(SignInViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Refresh();
    }

    // to disable go back functionality
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}