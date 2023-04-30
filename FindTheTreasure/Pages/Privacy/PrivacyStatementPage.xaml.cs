namespace FindTheTreasure.Pages.Privacy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivacyStatementPage : ContentPage
    {
        public PrivacyStatementPage(PrivacyStatementPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}