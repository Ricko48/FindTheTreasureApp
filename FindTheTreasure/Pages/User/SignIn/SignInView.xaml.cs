namespace FindTheTreasure.Pages.User.SignIn;

public partial class SignInView : ContentPage
{
    public SignInView(SignInViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}