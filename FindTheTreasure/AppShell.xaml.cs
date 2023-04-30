namespace FindTheTreasure;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(FoundBeaconsView), typeof(FoundBeaconsView));
        Routing.RegisterRoute(nameof(ScoreBoardView), typeof(ScoreBoardView));
        Routing.RegisterRoute(nameof(AccountDetailView), typeof(AccountDetailView));
        Routing.RegisterRoute(nameof(BeaconDetailPage), typeof(BeaconDetailPage));
    }
}
