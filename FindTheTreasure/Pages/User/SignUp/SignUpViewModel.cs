using FindTheTreasure.Services.User;
using System.Windows.Input;

namespace FindTheTreasure.Pages.User.SignUp
{
    public class SignUpViewModel
    {
        private readonly UserService _userService;

        public UserModel UserModel { get; set; } = new();

        public ICommand SignInButtonClickedCommand { get; }
        public ICommand SignUpButtonClickedCommand { get; }

        public SignUpViewModel(UserService userService)
        {
            _userService = userService;

            SignInButtonClickedCommand = new Command(async () => await OnSignInButtonClickedAsync());
            SignUpButtonClickedCommand = new Command(async () => await OnSignUpButtonClickedAsync());
        }

        public async Task OnSignInButtonClickedAsync()
        {
            await Shell.Current.GoToAsync(nameof(SignInView), false);
        }

        public async Task OnSignUpButtonClickedAsync()
        {
            if (string.IsNullOrEmpty(UserModel.UserName) || string.IsNullOrEmpty(UserModel.FirstName) || string.IsNullOrEmpty(UserModel.LastName))
            {
                await Shell.Current.DisplayAlert("Alert", "Please fill in all the fields.", "Ok");
                return;
            }
            if (!await _userService.SignUpAsync(UserModel))
            {
                await Shell.Current.DisplayAlert("Alert", $"The username '{UserModel.UserName}' is already used.", "Ok");
                return;
            }
            await Shell.Current.GoToAsync(nameof(SignInView), false);
        }
    }
}
