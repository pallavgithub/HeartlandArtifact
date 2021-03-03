using HeartlandArtifact.Helpers;
using HeartlandArtifact.Interfaces;
using HeartlandArtifact.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
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
        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand SubmitBtnCommand { get; set; }
        public EnterOtpPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GoBackCommand = new DelegateCommand(GoBack);
            SubmitBtnCommand = new DelegateCommand(SubmitButtonClicked);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            IsFromForgotPassword = (bool)parameters["FromForgetPassword"];
            if (IsFromForgotPassword)
            {
                Email=(string)parameters["Email_Id"];
            }
        }
        public void GoBack()
        {
            NavigationService.GoBackAsync();
        }
        public async void SubmitButtonClicked()
        {            
            var Toast = DependencyService.Get<IMessage>();
            if (string.IsNullOrEmpty(Text1) || string.IsNullOrEmpty(Text2) || string.IsNullOrEmpty(Text3) || string.IsNullOrEmpty(Text4))
            {
                if (TimerText == "0:00")
                {
                    Toast.LongAlert("Request time out."); return;
                }
                else
                {
                    Toast.LongAlert("Please enter correct otp."); return;
                }
            }
            if (TimerText == "0:00")
            {
                Toast.LongAlert("Request time out."); return;
            }
            if (TimerText == "0:00")
            {
                Toast.LongAlert("Request time out."); return;
            }
            else
            {
                try
                {
                    IsBusy = true;
                    string OTP = Text1 + Text2 + Text3 + Text4;
                    if (IsFromForgotPassword)
                    {
                        var response = await new ApiData().PostData<UserModel>("user/ForgotPassword?EmailId=" + Email + "&Otp=" + OTP, true);
                        if (response != null && response.data != null)
                        {
                            if (response.status == "Success")
                            {
                                await NavigationService.NavigateAsync("ChangePasswordPage");
                            }                           
                        }
                        else
                        {
                            Toast.LongAlert(response.message);
                        }
                    }
                    else
                    {
                        App.SignUpDetails.Otp = OTP;
                        var response = await new ApiData().PostData<UserModel>("user/signup", App.SignUpDetails, true);
                        if (response != null && response.data != null && response.status == "Success")
                        {
                            Application.Current.Properties["IsLogedIn"] = true;
                            Application.Current.Properties["LogedInUserId"] = response.data.CmsUserId;
                            await Application.Current.SavePropertiesAsync();
                            App.User = new UserDataModel()
                            {
                                UserId = response.data.CmsUserId,
                                FirstName = response.data.FirstName,
                                LastName = response.data.LastName,
                                EmailId = response.data.EmailId,
                                Password = response.data.Password,
                                IsActive = true
                            };
                            Toast.LongAlert("Signup Successful.");
                            await NavigationService.NavigateAsync("HomePage");
                            App.SignUpDetails.Otp = string.Empty;
                        }
                        else
                        {
                            Toast.LongAlert(response.message);
                            App.SignUpDetails.Otp = string.Empty;
                        }
                    }
                    IsBusy = false;
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
                    Toast.LongAlert("Otp sent.");
                }
                IsBusy = false;
            }
            else
            {
                IsBusy = true;
                var response = await new ApiData().PostData<UserModel>("user/signup", App.SignUpDetails, true);
                if (response != null && response.data != null)
                {
                    Toast.LongAlert("Otp sent.");
                }
                IsBusy = false;
            }
        }
    }
}
