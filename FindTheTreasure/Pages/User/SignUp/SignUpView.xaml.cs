namespace FindTheTreasure.Pages.User.SignUp;

public partial class SignUpView : ContentPage
{
    private readonly SignUpViewModel _viewModel;

    public SignUpView(SignUpViewModel viewModel)
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