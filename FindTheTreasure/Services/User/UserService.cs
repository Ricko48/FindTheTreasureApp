using FindTheTreasure.Services.User.API;
using Refit;

namespace FindTheTreasure.Services.User
{
    public class UserService
    {
        private readonly IUserApiClient _userApiClient;

        public UserService(IUserApiClient userApiClient) => _userApiClient = userApiClient;

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
            //var userModel = await _userApiClient.SignInAsync(userName);

            var userModel = new UserModel // arrange for testing
            {
                UserName = userName,
                FirstName = "Peter",
                LastName = "Parker",
            };

            Preferences.Set("isUserLoggedIn", "true");
            Preferences.Set("userName", userModel.UserName);
            Preferences.Set("firstName", userModel.FirstName);
            Preferences.Set("lastName", userModel.LastName);

            return true;
        }

        public void SignOut()
        {
            Preferences.Set("isUserLoggedIn", "false");
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
            //await _userApiClient.DeleteAsync(Preferences.Get("userName", null));
            SignOut();
        }

        public async Task<bool> SignUpAsync(UserModel userModel)
        {
            //return await _userApiClient.SignUpAsync(userModel);
            return true;
        }
    }
}
