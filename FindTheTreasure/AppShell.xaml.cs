namespace FindTheTreasure;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(GamesOverviewView), typeof(GamesOverviewView));
        Routing.RegisterRoute(nameof(GameCreateView), typeof(GameCreateView));
        Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
        Routing.RegisterRoute(nameof(FoundBeaconsView), typeof(FoundBeaconsView));
        Routing.RegisterRoute(nameof(ScoreBoardView), typeof(ScoreBoardView));
        Routing.RegisterRoute(nameof(UserDetailView), typeof(UserDetailView));
        Routing.RegisterRoute(nameof(SignInView), typeof(SignInView));
        Routing.RegisterRoute(nameof(SignUpView), typeof(SignUpView));
        Routing.RegisterRoute(nameof(BeaconDetailView), typeof(BeaconDetailView));
        Routing.RegisterRoute(nameof(InGameVIew), typeof(InGameVIew));
    }
}
