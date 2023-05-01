using System.Windows.Input;
using FindTheTreasure.Services.User;

namespace FindTheTreasure.Pages.User.SignIn
{
    public class SignInViewModel
    {
        private readonly UserService _userService;

        public string UserName { get; set; }

        public ICommand SignInButtonClickedCommand { get; }
        public ICommand SignUpButtonClickedCommand { get; }

        public SignInViewModel(UserService userService)
        {
            _userService = userService;

            SignInButtonClickedCommand = new Command(async () => await OnSignInButtonClickedAsync());
            SignUpButtonClickedCommand = new Command(async () => await OnSignUpButtonClickedAsync());
        }

        public async Task OnSignInButtonClickedAsync()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                await Shell.Current.DisplayAlert("Alert", "Please fill in the Username.", "Ok");
                return;
            }

            if (!await _userService.SignInAsync(UserName))
            {
                await Shell.Current.DisplayAlert("Alert", $"The username '{UserName}' is already used.", "Ok");
                return;
            }

            await Shell.Current.Navigation.PopToRootAsync();
            //await Shell.Current.GoToAsync(nameof(HomeView), false);
        }

        public async Task OnSignUpButtonClickedAsync()
        {
            await Shell.Current.GoToAsync( nameof(SignUpView), false);
        }
    }
}
