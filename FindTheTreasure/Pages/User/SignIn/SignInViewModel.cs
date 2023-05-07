using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FindTheTreasure.Services.User;
using Refit;

namespace FindTheTreasure.Pages.User.SignIn
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public ICommand SignInButtonClickedCommand { get; }
        public ICommand SignUpButtonClickedCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        public SignInViewModel(UserService userService)
        {
            _userService = userService;
            SignInButtonClickedCommand = new Command(async () => await OnSignInButtonClickedAsync());
            SignUpButtonClickedCommand = new Command(async () => await OnSignUpButtonClickedAsync());
        }

        public void Refresh()
        {
            UserName = null;
        }

        public async Task OnSignInButtonClickedAsync()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                await Shell.Current.DisplayAlert("Alert", "Please fill in the username.", "Ok");
                return;
            }

            try
            {
                await _userService.SignInAsync(UserName);

            } catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    await Shell.Current.DisplayAlert("Error", $"Account with username '{UserName}' was not found.", "Ok");
                    return;
                }

                await Shell.Current.DisplayAlert("Error", "Something went wrong.", "Ok");
                return;
            }

            await Shell.Current.Navigation.PopToRootAsync();
        }

        public async Task OnSignUpButtonClickedAsync()
        {
            await Shell.Current.GoToAsync( nameof(SignUpView), false);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
