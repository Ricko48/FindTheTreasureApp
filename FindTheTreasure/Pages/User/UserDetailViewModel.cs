using System.ComponentModel;
using System.Runtime.CompilerServices;
using FindTheTreasure.Services.User;
using System.Windows.Input;
using Refit;

namespace FindTheTreasure.Pages.User
{
    public class UserDetailViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;

        private UserModel _userModel;
        public UserModel UserModel
        {
            get => _userModel;
            set
            {
                _userModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand SignOutButtonClickedCommand { get; }
        public ICommand DeleteAccountButtonClickedCommand { get; }
        public ICommand SignInButtonClickedCommand { get; }
        public ICommand SignUpButtonClickedCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

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
            try
            {
                await _userService.DeleteAccountAsync();
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong.", "Ok");
                return;
            }
            
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
