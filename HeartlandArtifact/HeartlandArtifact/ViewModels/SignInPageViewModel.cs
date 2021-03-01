using HeartlandArtifact.Helpers;
using HeartlandArtifact.Interfaces;
using HeartlandArtifact.Models;
using HeartlandArtifact.Services.Contracts;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace HeartlandArtifact.ViewModels
{
    class SignInPageViewModel : ViewModelBase
    {
        private readonly IGoogleManager _googleManager;
        private readonly IFacebookManager _facebookManager;
        public DelegateCommand FacebookLoginCommand { get; set; }
        public DelegateCommand FacebookLogoutCommand { get; set; }
        public DelegateCommand GoogleLoginCommand { get; set; }
        public DelegateCommand GoogleLogoutCommand { get; set; }
        public DelegateCommand GoToSignUpPageCommand { get; set; }
        public DelegateCommand GoToForgotPasswordPageCommand { get; set; }
        public DelegateCommand ContinueCommand { get; set; }
        private FacebookUser _facebookUser;
        private GoogleUser _googleUser;
        private bool _isLogedIn;
        public bool IsLogedIn
        {
            get { return _isLogedIn; }
            set { SetProperty(ref _isLogedIn, value); }
        }
        private bool _isWorking;
        public bool IsWorking
        {
            get { return _isWorking; }
            set { SetProperty(ref _isWorking, value); }
        }

        private bool _showPassword;

        public bool ShowPassword
        {
            get { return _showPassword; }
            set { SetProperty(ref _showPassword, value); }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        public FacebookUser FacebookUser
        {
            get { return _facebookUser; }
            set { SetProperty(ref _facebookUser, value); }
        }
        public GoogleUser GoogleUser
        {
            get { return _googleUser; }
            set { SetProperty(ref _googleUser, value); }
        }
        public SignInPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _facebookManager = DependencyService.Get<IFacebookManager>();
            _googleManager = DependencyService.Get<IGoogleManager>();

            IsLogedIn = false;
            FacebookLoginCommand = new DelegateCommand(FacebookLogin);
            GoogleLoginCommand = new DelegateCommand(GoogleLogin);
            FacebookLogoutCommand = new DelegateCommand(FacebookLogout);
            GoogleLogoutCommand = new DelegateCommand(GoogleLogout);
            GoToSignUpPageCommand = new DelegateCommand(GoToSignUpPage);
            GoToForgotPasswordPageCommand = new DelegateCommand(GoToForgotPasswordPage);
            ContinueCommand = new DelegateCommand(SignIn);
        }
        public void GoToSignUpPage()
        {
            NavigationService.NavigateAsync("SignUpPage");
        }
        public void GoToForgotPasswordPage()
        {
            NavigationService.NavigateAsync("ForgotPasswordPage");
        }
        private void FacebookLogout()
        {
            _facebookManager.Logout();
            IsLogedIn = false;
        }
        private void FacebookLogin()
        {
            _facebookManager.Login(OnFacebookLoginComplete);
        }
        private void GoogleLogout()
        {
            _googleManager.Logout();
            IsLogedIn = false;
        }
        private void GoogleLogin()
        {
            _googleManager.Login(OnGoogleLoginComplete);
        }
        private void OnFacebookLoginComplete(FacebookUser facebookUser, string message)
        {
            var toast = DependencyService.Get<IMessage>();
            if (facebookUser != null)
            {
                FacebookUser = facebookUser;
                IsLogedIn = true;
                Application.Current.Properties["IsLogedIn"] = true;
                NavigationService.NavigateAsync("HomePage");
            }
            else
            {
                toast.LongAlert(message);
                //_dialogService.DisplayAlertAsync("Error", message, "Ok");
            }
        }
        private void OnGoogleLoginComplete(GoogleUser googleUser, string message)
        {
            var toast = DependencyService.Get<IMessage>();
            if (googleUser != null)
            {
                GoogleUser = googleUser;
                IsLogedIn = true;
                Application.Current.Properties["IsLogedIn"] = true;
                NavigationService.NavigateAsync("HomePage");
            }
            else
            {
                toast.LongAlert(message);
                //_dialogService.DisplayAlertAsync("Error", message, "Ok");
            }
        }
        public async void SignIn()
        {
            try
            {
                var toast = DependencyService.Get<IMessage>();
                if (string.IsNullOrEmpty(Email))
                {
                    toast.LongAlert("Please enter Email."); return;
                }
                //if (!Regex.IsMatch(Email.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                if (!Regex.IsMatch(Email.Trim(), @"/ ^((?:[a - zA - Z0 - 9] +) | (([a - zA - Z0 - 9] + (\.|\+|\-| _))+[a - zA - Z0 - 9] +))@(([a - zA - Z0 - 9] + (\.|\-))+[a - zA - Z]{ 2,4})$/ gm", RegexOptions.IgnoreCase))
                {
                    toast.LongAlert("Invalid email address."); return;
                }
                if (string.IsNullOrEmpty(Password))
                {
                    toast.LongAlert("Please enter Password."); return;
                }
                //if (Password.Trim().Length < 8)
                //{
                //    toast.LongAlert("Password must be at least 8 characters, no more than 15 characters."); return;
                //}
                if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", RegexOptions.None)|| Password.Trim().Length < 8)
                {
                   // toast.LongAlert("Password must include at least one uppercase letter, one lowercase letter, one numeric digit, one special character."); return;
                    toast.LongAlert("Password must be between 8 to 15 characters, including uppercase, lowercase letters, numbers, and special characters."); return;
                }
                else
                {
                    IsWorking = true;
                    var loginDetails = new LoginModel()
                    {
                        UserName = Email.Trim(),
                        Password = Password.Trim()
                    };
                    var response = await new ApiData().PostData<UserModel>("user/login", loginDetails, true);
                    if (response != null && response.data != null)
                    {
                        if (!(Password == response.data.Password))
                        {
                            toast.LongAlert("Password does not match");
                        }
                        else
                        {
                            Application.Current.Properties["IsLogedIn"] = true;
                            await Application.Current.SavePropertiesAsync();
                            toast.LongAlert(response.message);
                            // Application.Current.Properties["IsLoogedIn"] = true;
                            // await Application.Current.SavePropertiesAsync();
                            // if(App.userModel!=null)
                            await NavigationService.NavigateAsync("HomePage");
                        }
                    }
                    else
                    {
                        toast.LongAlert(response.message);
                    }
                    IsWorking = false;
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
