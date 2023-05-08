using FindTheTreasure.Services.User.API;

namespace FindTheTreasure.Services.User
{
    public class UserService
    {
        private readonly IUserApiClient _userApiClient;

        public UserService(IUserApiClient userApiClient) => _userApiClient = userApiClient;

        public bool IsSignedIn()
        {
            var isUserLoggedIn = Preferences.Get("isUserLoggedIn", null);
            return isUserLoggedIn == "true";
        }

        public async Task SignInAsync(string userName)
        {
            var userModel = await _userApiClient.GetUserAsync(userName);
            Preferences.Set("isUserLoggedIn", "true");
            Preferences.Set("userName", userModel.UserName);
            Preferences.Set("firstName", userModel.FirstName);
            Preferences.Set("lastName", userModel.LastName);
            Preferences.Set("userId", userModel.Id.ToString());
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

            var participantId = Preferences.Get("participantId", null);

            var user = new UserModel
            {
                UserName = Preferences.Get("userName", null),
                FirstName = Preferences.Get("firstName", null),
                LastName = Preferences.Get("lastName", null),
                Id = int.Parse(Preferences.Get("userId", null)),
                ParticipantId = !string.IsNullOrEmpty(participantId) ? int.Parse(participantId) : null
            };
            return user;
        }

        public async Task DeleteAccountAsync()
        {
            await _userApiClient.DeleteAsync(Preferences.Get("userName", null));
            SignOut();
        }

        public async Task SignUpAsync(UserModel userModel)
        {
            await _userApiClient.CreateUserAsync(userModel);
        }
    }
}
