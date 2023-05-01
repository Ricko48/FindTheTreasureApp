using FindTheTreasure.Services.User;
using System.Windows.Input;

namespace FindTheTreasure.Pages.User
{
    public class UserDetailViewModel
    {
        private readonly UserService _userService;

        public UserModel UserModel { get; set; }
        public bool IsSignedOut => !_userService.IsSignedIn();
        public bool IsSignedIn => _userService.IsSignedIn();

        public ICommand SignOutButtonClickedCommand { get; }
        public ICommand DeleteAccountButtonClickedCommand { get; }
        public ICommand SignInButtonClickedCommand { get; }
        public ICommand SignUpButtonClickedCommand { get; }

        public UserDetailViewModel(UserService userService)
        {
            _userService = userService;
            
            UserModel = _userService.GetUser();

            SignOutButtonClickedCommand = new Command(async () => await OnSignOutButtonClickedAsync());
            DeleteAccountButtonClickedCommand = new Command(async () => await OnDeleteAccountButtonClickedAsync());
            SignInButtonClickedCommand = new Command(async () => await OnSignInButtonClickedAsync());
            SignUpButtonClickedCommand = new Command(async () => await OnSignUpButtonClickedAsync());
        }

        public void Refresh()
        {
            UserModel = _userService.GetUser();
        }

        public async Task OnSignOutButtonClickedAsync()
        {
            _userService.SignOut();
            await Shell.Current.GoToAsync(nameof(SignInView), false);
        }

        public async Task OnDeleteAccountButtonClickedAsync()
        {
            await _userService.DeleteAccountAsync();
            await Shell.Current.GoToAsync(nameof(SignInView), false);
        }

        public async Task OnSignInButtonClickedAsync()
        {
            await Shell.Current.GoToAsync(nameof(SignInView), false);
        }

        public async Task OnSignUpButtonClickedAsync()
        {
            await Shell.Current.GoToAsync(nameof(SignUpView), false);
        }
    }
}
