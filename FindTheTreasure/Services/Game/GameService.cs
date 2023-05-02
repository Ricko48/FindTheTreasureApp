namespace FindTheTreasure.Services.Game
{
    public class GameService
    {
        public async Task<bool> StartGameAsync(string gameId)
        {
            // ToDo API

            Preferences.Set("isInGame", "true");
            Preferences.Set("gameId", gameId);
            return true;
        }

        public async Task<bool> StopGameAsync()
        {
            // ToDo API

            Preferences.Set("isInGame", "false");
            return true;
        }

        public bool IsInGame()
        {
            return Preferences.Get("isInGame", null) == "true";
        }

        public string GetGameId()
        {
            return Preferences.Get("gameId", null);
        }
    }
}
