namespace FindTheTreasure;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(FoundBeaconsPage), typeof(FoundBeaconsPage));
        Routing.RegisterRoute(nameof(ScoreBoardPage), typeof(ScoreBoardPage));
        Routing.RegisterRoute(nameof(AccountDetailPage), typeof(AccountDetailPage));
        Routing.RegisterRoute(nameof(BeaconDetailPage), typeof(BeaconDetailPage));
    }
}
