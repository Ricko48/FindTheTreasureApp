namespace FindTheTreasure.Pages.Beacon
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BeaconDetailPage : ContentPage
    {
        public BeaconDetailPage(BeaconDetailPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}