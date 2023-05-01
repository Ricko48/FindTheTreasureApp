namespace FindTheTreasure.Services.User
{
    public class UserService
    {
        public bool IsSignedIn()
        {
            var isUserLoggedIn = Preferences.Get("isUserLoggedIn", null);
            if (string.IsNullOrEmpty(isUserLoggedIn) || isUserLoggedIn == "false")
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SignInAsync(string userName)
        {

            // ToDo API

            var model = new UserModel // arrange
            {
                UserName = userName,
                FirstName = "Peter",
                LastName = "Parker",
            };

            Preferences.Set("isUserLoggedIn", "true");
            Preferences.Set("userName", model.UserName);
            Preferences.Set("firstName", model.FirstName);
            Preferences.Set("lastName", model.LastName);

            return true;
        }

        public void SignOut()
        {
            Preferences.Set("isUserLoggedIn", "false");
        }

        public string GetUserName()
        {
            if (!IsSignedIn())
            {
                return null;
            }
            return Preferences.Get("userName", null);
        }

        public UserModel GetUser()
        {
            if (!IsSignedIn())
            {
                return null;
            }
            return new UserModel
            {
                UserName = Preferences.Get("userName", null),
                FirstName = Preferences.Get("firstName", null),
                LastName = Preferences.Get("lastName", null)
            };
        }

        public async Task DeleteAccountAsync()
        {
            // ToDo API

            SignOut();
        }

        public async Task<bool> SignUpAsync(UserModel userModel)
        {
            // ToDo API

            return true;
        }
    }
}
