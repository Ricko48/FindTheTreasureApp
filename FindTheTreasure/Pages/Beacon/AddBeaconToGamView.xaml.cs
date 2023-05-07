using AndroidX.Lifecycle;

namespace FindTheTreasure.Pages.Beacon
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BeaconDetailView : ContentPage
    {
        private AddBeaconToGameViewModel ViewModel;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Refresh();
        }

        public BeaconDetailView(AddBeaconToGameViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}