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
    public class SignUpPageViewModel : ViewModelBase
    {
        public bool IsValid { get; set; }
        private readonly IGoogleManager _googleManager;
        private readonly IFacebookManager _facebookManager;
        private FacebookUser _facebookUser;
        private GoogleUser _googleUser;
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
        private bool _isLogedIn;
        public bool IsLogedIn
        {
            get { return _isLogedIn; }
            set { SetProperty(ref _isLogedIn, value); }
        }
        private bool _showPassword;
        public bool ShowPassword
        {
            get { return _showPassword; }
            set { SetProperty(ref _showPassword, value); }
        }
        private bool _showConfirmPassword;
        public bool ShowConfirmPassword
        {
            get { return _showConfirmPassword; }
            set { SetProperty(ref _showConfirmPassword, value); }
        }
        private bool _isAgree;
        public bool IsAgree
        {
            get { return _isAgree; }
            set { SetProperty(ref _isAgree, value); }
        }
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
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
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }
        public DelegateCommand FacebookLoginCommand { get; set; }
        public DelegateCommand FacebookLogoutCommand { get; set; }
        public DelegateCommand GoogleLoginCommand { get; set; }
        public DelegateCommand GoogleLogoutCommand { get; set; }
        public DelegateCommand GoBackToSignInPage { get; set; }
        public DelegateCommand SubmitBtnCommand { get; set; }
        public SignUpPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _facebookManager = DependencyService.Get<IFacebookManager>();
            _googleManager = DependencyService.Get<IGoogleManager>();

            IsLogedIn = false;
            FacebookLoginCommand = new DelegateCommand(FacebookLogin);
            GoogleLoginCommand = new DelegateCommand(GoogleLogin);
            FacebookLogoutCommand = new DelegateCommand(FacebookLogout);
            GoogleLogoutCommand = new DelegateCommand(GoogleLogout);
            GoBackToSignInPage = new DelegateCommand(GoToSignInPage);
            SubmitBtnCommand = new DelegateCommand(GoToEnterOtpPage);
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
            if (facebookUser != null)
            {
                FacebookUser = facebookUser;
                IsLogedIn = true;
            }
            else
            {
                //_dialogService.DisplayAlertAsync("Error", message, "Ok");
            }
        }
        private void OnGoogleLoginComplete(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                GoogleUser = googleUser;
                IsLogedIn = true;
            }
            else
            {
                //_dialogService.DisplayAlertAsync("Error", message, "Ok");
            }
        }
        public void GoToSignInPage()
        {
            NavigationService.GoBackAsync();
        }
        public async void GoToEnterOtpPage()
        {
            try
            {
                ValidateForm();
                if (IsValid)
                {
                    IsBusy = true;
                    App.SignUpDetails = new UserModel()
                    {
                        FirstName = FirstName.Trim(),
                        LastName = LastName.Trim(),
                        EmailId = Email.Trim(),
                        Password = Password.Trim()
                    };
                    var response = await new ApiData().PostData<UserModel>("user/signup", App.SignUpDetails, true);
                    if (response != null && response.data != null)
                    {                        
                        var navigationParams = new NavigationParameters();
                        navigationParams.Add("FromForgetPassword", false);
                        await NavigationService.NavigateAsync("EnterOtpPage", navigationParams);
                    }
                    IsBusy = false;
                }
            }
            catch (Exception e)
            {

            }
        }
        private void ValidateForm()
        {
            IsValid = true;
            var Toast = DependencyService.Get<IMessage>();
            if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName) && string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(ConfirmPassword))
            {
                Toast.LongAlert("All fields are required."); IsValid = false; return;
            }
            if (string.IsNullOrEmpty(FirstName))
            {
                Toast.LongAlert("FirstName is required."); IsValid = false; return;
            }
            if (!Regex.IsMatch(FirstName.Trim(), @"^(?=(?:[^A-Za-z]*[A-Za-z]){3})(?![^\d~`?!^*¨ˆ;@=$%{}\[\]|\/<>#“.,]*[\d~`?!^*¨ˆ;@=$%{}\[\]|\/<>#“.,])\S+(?: \S+){0,2}$", RegexOptions.IgnoreCase))
            {
                Toast.LongAlert("Full name is not valid."); IsValid = false; return;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                Toast.LongAlert("LastName is required."); IsValid = false; return;
            }
            if (!Regex.IsMatch(LastName.Trim(), @"^(?=(?:[^A-Za-z]*[A-Za-z]){3})(?![^\d~`?!^*¨ˆ;@=$%{}\[\]|\/<>#“.,]*[\d~`?!^*¨ˆ;@=$%{}\[\]|\/<>#“.,])\S+(?: \S+){0,2}$", RegexOptions.IgnoreCase))
            {
                Toast.LongAlert("LastName is not valid."); IsValid = false; return;
            }
            if (string.IsNullOrEmpty(Email))
            {
                Toast.LongAlert("Email is required."); IsValid = false; return;
            }
            if (!Regex.IsMatch(Email.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                Toast.LongAlert("Invalid email address."); IsValid = false; return;
            }
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Password.Trim()))
            {
                Toast.LongAlert("Password is required."); IsValid = false; return;
            }
            if (Password.Trim().Length < 8)
            {
                Toast.LongAlert("Password must be at least 8 characters, no more than 15 characters."); IsValid = false; return;
            }
            if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", RegexOptions.None))
            {
                Toast.LongAlert("Password must include at least one uppercase letter, one lowercase letter, one numeric digit, one special character."); IsValid = false; return;
            }
            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                Toast.LongAlert("Confirm password is required."); IsValid = false; return;
            }
            if (Password != ConfirmPassword)
            {
                Toast.LongAlert("Confirm password not match."); IsValid = false; return;
            }
            if (IsAgree == false)
            {
                Toast.LongAlert("Please accept Terms and Conditions"); IsValid = false; return;
            }
        }
    }
}
