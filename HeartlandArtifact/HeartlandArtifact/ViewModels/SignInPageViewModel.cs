using HeartlandArtifact.Helpers;
using HeartlandArtifact.Interfaces;
using HeartlandArtifact.Models;
using HeartlandArtifact.Services.Contracts;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace HeartlandArtifact.ViewModels
{
    class SignInPageViewModel : ViewModelBase
    {
        private readonly IGoogleManager _googleManager;
        private readonly IFacebookManager _facebookManager;
        private readonly IAppleManager _appleManager;
        public DelegateCommand AppleLoginCommand { get; set; }
        public DelegateCommand AppleLogoutCommand { get; set; }
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
            _appleManager = DependencyService.Get<IAppleManager>();

            IsLogedIn = false;
            AppleLoginCommand = new DelegateCommand(AppleIdLogin);
            AppleLogoutCommand = new DelegateCommand(AppleIdLogout);
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
        private void AppleIdLogout()
        {
           // _facebookManager.Logout();
            IsLogedIn = false;
        }
        private async void AppleIdLogin()
        {
            //var k = _appleManager.IsAvailable;
            var account = await  _appleManager.SignInAsync();
            
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
        private async void OnFacebookLoginComplete(FacebookUser facebookUser, string message)
        {
            var toast = DependencyService.Get<IMessage>();
            if (facebookUser != null)
            {
                FacebookUser = facebookUser;
                HttpClient client = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();
                form.Add(new StringContent(FacebookUser.FirstName), "FirstName");
                form.Add(new StringContent(FacebookUser.LastName), "LastName");
                form.Add(new StringContent(FacebookUser.Email), "EmailId");
                form.Add(new StringContent("Facebook"), "Platform");
                form.Add(new StringContent(string.Empty), "ContactNumber");
                var response = await new ApiData().PostFormData<UserModel>("user/SocialMediaLogin", form, true);
                if (response != null && response.data != null)
                {
                    string newString = new String(response.data.FirstName.Select((ch, index) => (index == 0) ? Char.ToUpper(ch) : Char.ToLower(ch)).ToArray());
                    IsLogedIn = true;
                    Application.Current.Properties["IsLogedIn"] = true;
                    Application.Current.Properties["LogedInUserId"] = response.data.CmsUserId;
                    Application.Current.Properties["UserName"] = newString;
                    await Application.Current.SavePropertiesAsync();
                    toast.LongAlert("Login Successful ! Welcome to Relic Collector.");
                    await NavigationService.NavigateAsync("/HomePage");
                }
                else
                {
                    toast.LongAlert(response.message);
                }
            }
            else
            {
                toast.LongAlert(message);
            }
        }
        private async void OnGoogleLoginComplete(GoogleUser googleUser, string message)
        {
            var toast = DependencyService.Get<IMessage>();
            if (googleUser != null)
            {
                GoogleUser = googleUser;
                MultipartFormDataContent form = new MultipartFormDataContent();

                form.Add(new StringContent(GoogleUser.Name.Split(' ')[0]), "FirstName");
                form.Add(new StringContent(GoogleUser.Name.Split(' ')[1]), "LastName");
                form.Add(new StringContent(GoogleUser.Email), "EmailId");
                form.Add(new StringContent("Google"), "Platform");
                form.Add(new StringContent(string.Empty), "ContactNumber");
                var response = await new ApiData().PostFormData<UserModel>("user/SocialMediaLogin", form, true);
                if (response != null && response.data != null)
                {
                    IsLogedIn = true;
                    string newString = new String(response.data.FirstName.Select((ch, index) => (index == 0) ? Char.ToUpper(ch) : Char.ToLower(ch)).ToArray());
                    Application.Current.Properties["IsLogedIn"] = true;
                    Application.Current.Properties["LogedInUserId"] = response.data.CmsUserId;
                    Application.Current.Properties["UserName"] = newString;
                    await Application.Current.SavePropertiesAsync();
                    toast.LongAlert("Login Successful ! Welcome to Relic Collector.");
                    await NavigationService.NavigateAsync("/HomePage");
                }
                else
                {
                    toast.LongAlert(response.message);
                }
            }
            else
            {
                toast.LongAlert(message);
            }
        }
        public async void SignIn()
        {
            try
            {
                var toast = DependencyService.Get<IMessage>();
                if (string.IsNullOrEmpty(Email))
                {
                    toast.LongAlert("Please enter your email address."); return;
                }
                //if (!Regex.IsMatch(Email.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                if (!Regex.IsMatch(Email.Trim(), @"^((?:[a-zA-Z0-9]+)|(([a-zA-Z0-9]+(\.|\+|\-|_))+[a-zA-Z0-9]+))@(([a-zA-Z0-9]+(\.|\-))+[a-zA-Z]{2,4})$", RegexOptions.IgnoreCase))
                {
                    toast.LongAlert("Oops, this email address doesn't look right."); return;
                }
                if (string.IsNullOrEmpty(Password))
                {
                    toast.LongAlert("Please enter your password."); return;
                }
                //if (Password.Trim().Length < 8)
                //{
                //    toast.LongAlert("Password must be at least 8 characters, no more than 15 characters."); return;
                //}
                if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", RegexOptions.None) || Password.Trim().Length < 8)
                {
                    // toast.LongAlert("Password must include at least one uppercase letter, one lowercase letter, one numeric digit, one special character."); return;
                    toast.LongAlert("Invalid Password. You could try resetting your password."); return;
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
                            toast.LongAlert("Invalid Password. You could try resetting your password.");
                        }
                        else if (response.status.ToLower() == "success")
                        {
                            string newString = new String(response.data.FirstName.Select((ch, index) => (index == 0) ? Char.ToUpper(ch) : Char.ToLower(ch)).ToArray());
                            Application.Current.Properties["IsLogedIn"] = true;
                            Application.Current.Properties["LogedInUserId"] = response.data.CmsUserId;
                            Application.Current.Properties["UserName"] = newString;
                            await Application.Current.SavePropertiesAsync();
                            toast.LongAlert("Login Successful! Welcome to Relic Collector.");
                            await NavigationService.NavigateAsync("/HomePage");
                        }
                        else
                        {
                            toast.LongAlert(response.message);
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
