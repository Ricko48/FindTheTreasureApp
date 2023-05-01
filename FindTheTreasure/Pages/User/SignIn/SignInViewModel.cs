using FindTheTreasure.Services.User;

namespace FindTheTreasure.Pages.User.SignIn
{
    public class SignInViewModel
    {
        private readonly UserService _userService;

        public SignInViewModel(UserService userService) => _userService = userService;


    }
}
