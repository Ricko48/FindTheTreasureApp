using System.ComponentModel;
using System.Runtime.CompilerServices;
using FindTheTreasure.Services.User;
using System.Windows.Input;

namespace FindTheTreasure.Pages.User.SignUp
{
    public class SignUpViewModel : INotifyPropertyChanged
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

        public ICommand SignInButtonClickedCommand { get; }
        public ICommand SignUpButtonClickedCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public SignUpViewModel(UserService userService)
        {
            _userService = userService;

            SignInButtonClickedCommand = new Command(async () => await OnSignInButtonClickedAsync());
            SignUpButtonClickedCommand = new Command(async () => await OnSignUpButtonClickedAsync());
        }

        public void Refresh()
        {
            UserModel = new UserModel();
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
