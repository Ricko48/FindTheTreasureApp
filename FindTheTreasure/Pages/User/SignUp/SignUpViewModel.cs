using FindTheTreasure.Services.User;

namespace FindTheTreasure.Pages.User.SignUp
{
    public class SignUpViewModel
    {
        private readonly UserService _userService;

        public SignUpViewModel(UserService userService) => _userService = userService;


    }
}
