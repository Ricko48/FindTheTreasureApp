namespace FindTheTreasure.Pages.Beacon
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BeaconDetailView : ContentPage
    {
        public BeaconDetailView(BeaconDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}