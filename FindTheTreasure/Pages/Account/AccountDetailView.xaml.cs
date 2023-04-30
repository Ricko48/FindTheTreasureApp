namespace FindTheTreasure.Pages.Account;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AccountDetailView : ContentPage
{
	public AccountDetailView(AccountDetailViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}