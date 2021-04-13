using HeartlandArtifact.Helpers;
using HeartlandArtifact.Interfaces;
using HeartlandArtifact.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;

namespace HeartlandArtifact.ViewModels
{
    public class EnterOtpPageViewModel : ViewModelBase
    {
        private bool _isFromForgotPassword;
        public bool IsFromForgotPassword
        {
            get { return _isFromForgotPassword; }
            set { SetProperty(ref _isFromForgotPassword, value); }
        }
        private bool _isFbEmailVerification;
        public bool IsFbEmailVerification
        {
            get { return _isFbEmailVerification; }
            set { SetProperty(ref _isFbEmailVerification, value); }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        private string _text1;
        public string Text1
        {
            get { return _text1; }
            set { SetProperty(ref _text1, value); }
        }
        private string _text2;
        public string Text2
        {
            get { return _text2; }
            set { SetProperty(ref _text2, value); }
        }
        private string _text3;
        public string Text3
        {
            get { return _text3; }
            set { SetProperty(ref _text3, value); }
        }
        private string _text4;
        public string Text4
        {
            get { return _text4; }
            set { SetProperty(ref _text4, value); }
        }
        private string _timerText;
        public string TimerText
        {
            get { return _timerText; }
            set { SetProperty(ref _timerText, value); }
        }
        public INavigationService _nav;
        public EnterOtpPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _nav = navigationService;
            // GoBackCommand = new DelegateCommand(GoBack);
            // SubmitBtnCommand = new DelegateCommand(SubmitButtonClicked);
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            //IsFromForgotPassword = (bool)parameters["FromForgetPassword"];
            if (parameters.ContainsKey("FromForgetPassword"))
            {
                IsFromForgotPassword = (bool)parameters["FromForgetPassword"];
                if (IsFromForgotPassword)
                {
                    Email = (string)parameters["Email_Id"];
                }
            }
            else if (parameters.ContainsKey("FromResetPassword"))
            {
                if ((bool)parameters["FromResetPassword"])
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        await _nav.GoBackAsync();
                    }
                }
            }
            else if (parameters.ContainsKey("IsFbEmailVerification"))
            {
                if ((bool)parameters["IsFbEmailVerification"])
                {
                    IsFbEmailVerification = true;
                }
            }
        }
        public async void SubmitButtonClicked()
        {
            string OTP = Text1 + Text2 + Text3 + Text4;
            var Toast = DependencyService.Get<IMessage>();
            if (TimerText == "0:00")
            {
                Toast.LongAlert("Oops, request timeout. Please tap on resend OTP."); return;
            }
            if (string.IsNullOrEmpty(Text1) && string.IsNullOrEmpty(Text2) && string.IsNullOrEmpty(Text3) && string.IsNullOrEmpty(Text4))
            {
                Toast.LongAlert("Please enter OTP sent to your entered email id."); return;
            }
            else
            {
                try
                {
                    if (IsFromForgotPassword)
                    {
                        IsBusy = true;
                        var response = await new ApiData().PostData<UserModel>("user/ForgotPassword?EmailId=" + Email + "&Otp=" + OTP, true);
                        if (response != null && response.data != null)
                        {
                            if (response.status == "Success")
                            {
                                var navigationParams = new NavigationParameters();
                                navigationParams.Add("EmailId", Email);
                                await NavigationService.NavigateAsync("ChangePasswordPage", navigationParams);
                            }
                        }
                        else
                        {
                            Toast.LongAlert(response.message);
                        }
                        IsBusy = false;
                    }
                    else if (IsFbEmailVerification)
                    {
                        if (App.FacebookUserDetails.Otp != OTP)
                        {
                            Toast.LongAlert("Entered OTP does not match."); return;
                        }
                        else
                        {
                            IsBusy = true;
                            if (App.FacebookUserDetails != null)
                            {
                                MultipartFormDataContent form = new MultipartFormDataContent();
                                form.Add(new StringContent(App.FacebookUserDetails.FacebookId), "FacebookId");
                                form.Add(new StringContent(App.FacebookUserDetails.FirstName), "FirstName");
                                form.Add(new StringContent(App.FacebookUserDetails.LastName), "LastName");
                                form.Add(new StringContent(App.FacebookUserDetails.EmailId ?? ""), "EmailId");
                                form.Add(new StringContent("Facebook"), "Platform");
                                form.Add(new StringContent(string.Empty), "ContactNumber");
                                var response = await new ApiData().PostFormData<UserModel>("user/SocialMediaLogin", form, true);
                                if (response != null && response.data != null)
                                {
                                    string newString = new String(response.data.FirstName.Select((ch, index) => (index == 0) ? Char.ToUpper(ch) : Char.ToLower(ch)).ToArray());
                                    Application.Current.Properties["IsLogedIn"] = true;
                                    Application.Current.Properties["LogedInUserId"] = response.data.CmsUserId;
                                    Application.Current.Properties["UserName"] = newString;
                                    await Application.Current.SavePropertiesAsync();
                                    Toast.LongAlert("Welcome to Relic Collector.");
                                    await NavigationService.NavigateAsync("HomePage");
                                    App.FacebookUserDetails.Otp = string.Empty;
                                }
                                else
                                {
                                    Toast.LongAlert(response.message);
                                }
                                IsBusy = false;
                            }
                        }
                    }
                    else
                    {
                        if (App.SignUpDetails.Otp != OTP)
                        {
                            Toast.LongAlert("Entered OTP does not match."); return;
                        }
                        else
                        {
                            IsBusy = true;
                            var response = await new ApiData().PostData<UserModel>("User/SignupWithVerifiedOtp", App.SignUpDetails, true);
                            if (response != null && response.data != null && response.status == "Success")
                            {
                                string newString = new String(response.data.FirstName.Select((ch, index) => (index == 0) ? Char.ToUpper(ch) : Char.ToLower(ch)).ToArray());
                                Application.Current.Properties["IsLogedIn"] = true;
                                Application.Current.Properties["LogedInUserId"] = response.data.CmsUserId;
                                Application.Current.Properties["UserName"] = newString;
                                await Application.Current.SavePropertiesAsync();
                                Toast.LongAlert("Welcome to Relic Collector.");
                                await NavigationService.NavigateAsync("HomePage");
                                App.SignUpDetails.Otp = string.Empty;
                            }
                            else
                            {
                                Toast.LongAlert(response.message);
                                App.SignUpDetails.Otp = string.Empty;
                            }
                            IsBusy = false;
                        }
                    }

                }
                catch (Exception e)
                {

                }
            }

        }
        public async void ResendOtp()
        {
            var Toast = DependencyService.Get<IMessage>();
            Text1 = Text2 = Text3 = Text4 = string.Empty;
            if (IsFromForgotPassword)
            {
                IsBusy = true;
                var response = await new ApiData().PostData<UserModel>("user/ForgotPassword?EmailId=" + Email + "&Otp=" + string.Empty, true);
                if (response != null && response.data != null)
                {
                    Toast.LongAlert("We have sent an OTP to your entered email id. Please check your email inbox.");
                }
                IsBusy = false;
            }
            else if (IsFbEmailVerification)
            {
                IsBusy = true;
                var response = await new ApiData().PostData<UserModel>("User/VerifyFBEmail?Email=" + App.FacebookUserDetails.EmailId + "&otp=" + string.Empty, true);
                if (response != null && response.data != null)
                {
                    App.FacebookUserDetails.Otp = response.data.Otp;
                    Toast.LongAlert("We have sent an OTP to your entered email id. Please check your email inbox.");
                }
                IsBusy = false;
            }
            else
            {
                IsBusy = true;
                var response = await new ApiData().PostData<UserModel>("user/signup", App.SignUpDetails, true);
                if (response != null && response.data != null)
                {
                    App.SignUpDetails.Otp = response.data.Otp;
                    Toast.LongAlert("We have sent an OTP to your entered email id.Please check your email inbox.");
                }
                IsBusy = false;
            }
        }
    }
}
