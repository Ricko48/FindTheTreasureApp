using Preferences = Microsoft.Maui.Storage.Preferences;

namespace FindTheTreasure.Services.User
{
    public class UserService
    {
        public bool IsLoggedIn()
        {
            var isUserLoggedIn = Preferences.Get("isUserLoggedIn", null);
            if (isUserLoggedIn == null || isUserLoggedIn == "false")
            {
                return false;
            }
            return true;
        }

        public async Task<bool> LogInAsync(string userName)
        {
            if (IsLoggedIn())
            {
                return false;
            }

            // ToDo API

            Preferences.Set("isUserLoggedIn", "true");
            return true;
        }

        public bool LogOut()
        {
            if (!IsLoggedIn())
            {
                return false;
            }
            Preferences.Set("isUserLoggedIn", "false");
            return true;
        }
    }
}
