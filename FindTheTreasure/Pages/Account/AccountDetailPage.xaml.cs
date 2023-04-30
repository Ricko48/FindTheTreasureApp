namespace FindTheTreasure.Pages.Account;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AccountDetailPage : ContentPage
{
	public AccountDetailPage(AccountDetailPageModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}