using HeartlandArtifact.Helpers;
using HeartlandArtifact.Interfaces;
using HeartlandArtifact.Models;
using HeartlandArtifact.Services.Contracts;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace HeartlandArtifact.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        public bool IsValid { get; set; }
        private readonly IAppleManager _appleManager;
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
        private string _facebookEmailId;
        public string FacebookEmailId
        {
            get { return _facebookEmailId; }
            set { SetProperty(ref _facebookEmailId, value); }
        }
        private bool _facebookEmailPopupIsVisible;
        public bool FacebookEmailPopupIsVisible
        {
            get { return _facebookEmailPopupIsVisible; }
            set { SetProperty(ref _facebookEmailPopupIsVisible, value); }
        }
        public DelegateCommand FacebookLoginCommand { get; set; }
        public DelegateCommand FacebookLogoutCommand { get; set; }
        public DelegateCommand GoogleLoginCommand { get; set; }
        public DelegateCommand GoogleLogoutCommand { get; set; }
        public DelegateCommand GoBackToSignInPage { get; set; }
        public DelegateCommand SubmitBtnCommand { get; set; }
        public DelegateCommand AppleLoginCommand { get; set; }
        public DelegateCommand AppleLogoutCommand { get; set; }
        public DelegateCommand SubmitFbIdButtonCommand { get; set; }
        public DelegateCommand CrossButtonCommand { get; set; }
        public SignUpPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _facebookManager = DependencyService.Get<IFacebookManager>();
            _googleManager = DependencyService.Get<IGoogleManager>();
            _appleManager = DependencyService.Get<IAppleManager>();
            IsLogedIn = false;
            FacebookLoginCommand = new DelegateCommand(FacebookLogin);
            GoogleLoginCommand = new DelegateCommand(GoogleLogin);
            FacebookLogoutCommand = new DelegateCommand(FacebookLogout);
            AppleLoginCommand = new DelegateCommand(AppleIdLogin);
            AppleLogoutCommand = new DelegateCommand(AppleIdLogout);
            GoogleLogoutCommand = new DelegateCommand(GoogleLogout);
            GoBackToSignInPage = new DelegateCommand(GoToSignInPage);
            SubmitBtnCommand = new DelegateCommand(GoToEnterOtpPage);
            SubmitFbIdButtonCommand = new DelegateCommand(UpdateFacebookEmailId);
            CrossButtonCommand = new DelegateCommand(CloseEmailPopup);
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
        private void AppleIdLogout()
        {
            IsLogedIn = false;
        }
        private async void AppleIdLogin()
        {
            try
            {
                var account = await _appleManager.SignInAsync();
                var toast = DependencyService.Get<IMessage>();
                if (account != null)
                {
                    IsBusy = true;
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    if (string.IsNullOrEmpty(account.Name))
                    {
                        form.Add(new StringContent(account.Name.Split(' ')[0]), "FirstName");
                        form.Add(new StringContent(account.Name.Split(' ')[1]), "LastName");
                    }
                    else
                    {
                        form.Add(new StringContent(""), "FirstName");
                        form.Add(new StringContent(""), "LastName");
                    }
                    form.Add(new StringContent(account.UserId ?? ""), "AppleAccountId");
                    form.Add(new StringContent(account.Email), "EmailId");
                    form.Add(new StringContent("Apple"), "Platform");
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
                        toast.LongAlert("Welcome to Relic Collector.");
                        await NavigationService.NavigateAsync("/HomePage");
                    }
                    else
                    {
                        toast.LongAlert(response.message);
                    }
                    IsBusy = false;
                }
                else
                {
                    toast.LongAlert("Something went wrong.");
                }
            }
            catch (Exception e)
            {

            }
        }
        private async void OnFacebookLoginComplete(FacebookUser facebookUser, string message)
        {
            var toast = DependencyService.Get<IMessage>();
            if (facebookUser != null)
            {
                IsBusy = true;
                FacebookUser = facebookUser;
                HttpClient client = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();
                form.Add(new StringContent(FacebookUser.Id), "FacebookId");
                form.Add(new StringContent(FacebookUser.FirstName), "FirstName");
                form.Add(new StringContent(FacebookUser.LastName), "LastName");
                form.Add(new StringContent(FacebookUser.Email ?? ""), "EmailId");
                form.Add(new StringContent("Facebook"), "Platform");
                form.Add(new StringContent(string.Empty), "ContactNumber");
                var response = await new ApiData().PostFormData<UserModel>("user/SocialMediaLogin", form, true);
                if (response != null && response.data != null)
                {
                    if (response.status == "400")
                    {
                        FacebookEmailPopupIsVisible = true;
                    }
                    else
                    {
                        string newString = new String(response.data.FirstName.Select((ch, index) => (index == 0) ? Char.ToUpper(ch) : Char.ToLower(ch)).ToArray());
                        IsLogedIn = true;
                        Application.Current.Properties["IsLogedIn"] = true;
                        Application.Current.Properties["LogedInUserId"] = response.data.CmsUserId;
                        Application.Current.Properties["UserName"] = newString;
                        await Application.Current.SavePropertiesAsync();
                        toast.LongAlert("Welcome to Relic Collector.");
                        await NavigationService.NavigateAsync("/HomePage");
                    }
                }
                else
                {
                    toast.LongAlert(response.message);
                }
                IsBusy = false;
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
                IsBusy = true;
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
                    string newString = new String(response.data.FirstName.Select((ch, index) => (index == 0) ? Char.ToUpper(ch) : Char.ToLower(ch)).ToArray());
                    IsLogedIn = true;
                    Application.Current.Properties["IsLogedIn"] = true;
                    Application.Current.Properties["LogedInUserId"] = response.data.CmsUserId;
                    Application.Current.Properties["UserName"] = newString;
                    await Application.Current.SavePropertiesAsync();
                    toast.LongAlert("Welcome to Relic Collector.");
                    await NavigationService.NavigateAsync("/HomePage");
                }
                else
                {
                    toast.LongAlert(response.message);
                }
                IsBusy = false;
            }
            else
            {
                toast.LongAlert(message);
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
                    var Toast = DependencyService.Get<IMessage>();
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
                        Toast.LongAlert("We have sent an OTP to your entered email id. Please check your email inbox.");
                        App.SignUpDetails.Otp = response.data.Otp;
                        var navigationParams = new NavigationParameters();
                        navigationParams.Add("FromForgetPassword", false);
                        await NavigationService.NavigateAsync("EnterOtpPage", navigationParams);
                    }
                    else
                    {
                        Toast.LongAlert(response.message);
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
                Toast.LongAlert("All fields are mandatory."); IsValid = false; return;
            }
            if (string.IsNullOrEmpty(FirstName))
            {
                Toast.LongAlert("Please enter your first name."); IsValid = false; return;
            }
            if (FirstName.Length > 30 || !Regex.IsMatch(FirstName.Trim(), @"^(?=(?:[^A-Za-z]*[A-Za-z]){3})(?![^\d~`?!^*¨ˆ;@=$%{}\[\]|\/<>#“.,]*[\d~`?!^*¨ˆ;@=$%{}\[\]|\/<>#“.,])\S+(?: \S+){0,2}$", RegexOptions.IgnoreCase))
            {
                Toast.LongAlert("Are you sure you entered your first name correctly?"); IsValid = false; return;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                Toast.LongAlert("Please enter your last name."); IsValid = false; return;
            }
            if (LastName.Length > 30 || !Regex.IsMatch(LastName.Trim(), @"^(?=(?:[^A-Za-z]*[A-Za-z]){3})(?![^\d~`?!^*¨ˆ;@=$%{}\[\]|\/<>#“.,]*[\d~`?!^*¨ˆ;@=$%{}\[\]|\/<>#“.,])\S+(?: \S+){0,2}$", RegexOptions.IgnoreCase))
            {
                Toast.LongAlert("Are you sure you entered your last name correctly?"); IsValid = false; return;
            }
            if (string.IsNullOrEmpty(Email))
            {
                Toast.LongAlert("Please enter your email address."); IsValid = false; return;
            }
            if (!Regex.IsMatch(Email.Trim(), @"^((?:[a-zA-Z0-9]+)|(([a-zA-Z0-9]+(\.|\+|\-|_))+[a-zA-Z0-9]+))@(([a-zA-Z0-9]+(\.|\-))+[a-zA-Z]{2,4})$", RegexOptions.IgnoreCase))
            {
                Toast.LongAlert("Oops, that email address doesn't look right."); IsValid = false; return;
            }
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Password.Trim()))
            {
                Toast.LongAlert("Please enter new password."); IsValid = false; return;
            }
            //if (!Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", RegexOptions.None) || Password.Trim().Length < 8)
            if (!Regex.IsMatch(Password, @"^[a-zA-Z]{8,15}$", RegexOptions.None) || Password.Trim().Length < 8)
            {
                Toast.LongAlert("Password must be between 8 to 15 characters, including uppercase, lowercase letters."); IsValid = false; return;
            }
            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                Toast.LongAlert("Please re-enter your password."); IsValid = false; return;
            }
            if (Password != ConfirmPassword)
            {
                Toast.LongAlert("Hey, confirm password should be same as the new password."); IsValid = false; return;
            }
            if (IsAgree == false)
            {
                Toast.LongAlert("Please accept Terms and Conditions"); IsValid = false; return;
            }
        }
        public async void UpdateFacebookEmailId()
        {
            var toast = DependencyService.Get<IMessage>();
            if (string.IsNullOrEmpty(FacebookEmailId))
            {
                toast.LongAlert("Please enter your email address."); return;
            }
            else
            {
                IsBusy = true;
                var response = await new ApiData().PutData<UserModel>("User/UpdateFacebookSignUpEmail?FacebookId=" + FacebookUser.Id + "&Email=" + FacebookEmailId, true);
                if (response != null && response.data != null)
                {
                    // toast.LongAlert(response.message);
                    FacebookEmailPopupIsVisible = false;
                    string newString = new String(response.data.FirstName.Select((ch, index) => (index == 0) ? Char.ToUpper(ch) : Char.ToLower(ch)).ToArray());
                    IsLogedIn = true;
                    Application.Current.Properties["IsLogedIn"] = true;
                    Application.Current.Properties["LogedInUserId"] = response.data.CmsUserId;
                    Application.Current.Properties["UserName"] = newString;
                    await Application.Current.SavePropertiesAsync();
                    toast.LongAlert("Welcome to Relic Collector.");
                    await NavigationService.NavigateAsync("/HomePage");
                }
                IsBusy = false;
            }

        }
        public void CloseEmailPopup()
        {
            FacebookEmailPopupIsVisible = false;

        }
    }
}
