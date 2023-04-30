namespace FindTheTreasure;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(FoundBeaconsView), typeof(FoundBeaconsView));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(InstructionsPage), typeof(InstructionsPage));
        Routing.RegisterRoute(nameof(PrivacyStatementPage), typeof(PrivacyStatementPage));
        Routing.RegisterRoute(nameof(BeaconDetailPage), typeof(BeaconDetailPage));
    }
}
