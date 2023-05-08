using System.Windows.Input;
using FindTheTreasure.Services.Game;

namespace FindTheTreasure.Pages.Games.InGame
{
    public class InGameViewModel
    {
        private readonly GameService _gameService;

        public ICommand StopGameButtonClickedCommand { get; }
        public ICommand ShowMapButtonClickedCommand { get; }

        public InGameViewModel(GameService gameService)
        {
            _gameService = gameService;
            StopGameButtonClickedCommand = new Command(async () => await OnStopGameButtonClickedAsync());
            ShowMapButtonClickedCommand = new Command(async () => await OnShowMapButtonClickedAsync());
        }

        public async Task OnStopGameButtonClickedAsync()
        {
            await _gameService.StopGameAsync();
            await Shell.Current.Navigation.PopAsync();
        }

        public async Task OnShowMapButtonClickedAsync()
        {
            await Shell.Current.GoToAsync(nameof(FoundBeaconsView), false);
        }
    }
}
