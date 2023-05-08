using System.Windows.Input;
using FindTheTreasure.Services.Game;

namespace FindTheTreasure.Pages.Games.InGame
{
    public class InGameViewModel
    {
        private readonly GameService _gameService;
        private readonly FoundBeaconsView _mapView;

        public ICommand StopGameButtonClickedCommand { get; }
        public ICommand ShowMapButtonClickedCommand { get; }

        public InGameViewModel(GameService gameService, FoundBeaconsView mapView)
        {
            _mapView = mapView;
            _gameService = gameService;
            StopGameButtonClickedCommand = new Command(async () => await OnStopGameButtonClickedAsync());
            ShowMapButtonClickedCommand = new Command(async () => await OnShowMapButtonClickedAsync());
        }

        public async Task OnStopGameButtonClickedAsync()
        {
            await _gameService.StopGameAsync();
            await Shell.Current.Navigation.PopToRootAsync();
        }

        public async Task OnShowMapButtonClickedAsync()
        {
            await Shell.Current.Navigation.PushAsync(_mapView);
        }
    }
}
