namespace FindTheTreasure.Pages.User;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class UserDetailView : ContentPage
{
	public UserDetailView(UserDetailViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}