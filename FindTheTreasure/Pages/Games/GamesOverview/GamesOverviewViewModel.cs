using FindTheTreasure.Models;
using FindTheTreasure.Services.Game;
using FindTheTreasure.Services.User;
using System.Collections.ObjectModel;

namespace FindTheTreasure.Pages.Games.GamesOverview
{
    public class GamesOverviewViewModel : ObservableObject
    {
        public IAsyncRelayCommand GoCreateGameAsyncCommand { get; }
        public IAsyncRelayCommand GoToStartGamePageAsyncCommand { get; }
        private GameService GameService;

        public ObservableCollection<GameModel> Games { get; set; } = new ObservableCollection<GameModel>();
        public GamesOverviewViewModel(UserService userService, GameService gameService)
        {
            GameService = gameService;
            GoCreateGameAsyncCommand = new AsyncRelayCommand(GoToCreateGameAsync);
            GoToStartGamePageAsyncCommand = new AsyncRelayCommand<GameModel>(GoToStartGamePageAsync);

            if (!userService.IsSignedIn())
            {
                Shell.Current.GoToAsync(nameof(SignInView), false).Wait();
            }

            //GameService.StartGameAsync(1).Wait(); // test
            //Shell.Current.GoToAsync(nameof(InGameVIew), false).Wait(); // test

            //GetGames();

        }

        public async void GetGames()
        {
            Games.Clear();
            var games = await GameService.GetGamesAsync();
            games.ForEach(g => Games.Add(g));
        }

        private async Task GoToStartGamePageAsync(GameModel item)
        {
            //start a game
            bool answer = await Shell.Current.DisplayAlert("Start a game?", "Would you like to start a game", "Yes", "No");
            if (answer)
            {
                //go to page
            }
        }

        private async Task GoToCreateGameAsync()
        {
            await Shell.Current.GoToAsync(nameof(GameCreateView), true);
        }

    }
}
