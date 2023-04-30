namespace FindTheTreasure;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
        Routing.RegisterRoute(nameof(FoundBeaconsView), typeof(FoundBeaconsView));
        Routing.RegisterRoute(nameof(ScoreBoardView), typeof(ScoreBoardView));
        Routing.RegisterRoute(nameof(AccountDetailView), typeof(AccountDetailView));
        Routing.RegisterRoute(nameof(BeaconDetailView), typeof(BeaconDetailView));
    }
}
