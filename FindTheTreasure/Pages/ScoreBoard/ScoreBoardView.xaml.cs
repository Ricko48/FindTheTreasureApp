namespace FindTheTreasure.Pages.ScoreBoard;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ScoreBoardView : ContentPage
{

    public ScoreBoardViewModel ViewModel { get; set; }
    public ScoreBoardView(ScoreBoardViewModel viewModel = null)
    {
        BindingContext = viewModel;
        ViewModel = viewModel;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.Refresh();
    }
}