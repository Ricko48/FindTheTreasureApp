using AndroidX.Lifecycle;

namespace FindTheTreasure.Pages.User.SignUp;

public partial class SignUpView : ContentPage
{
    public SignUpView(SignUpViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}