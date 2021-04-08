using HeartlandArtifact.Helpers;
using HeartlandArtifact.Interfaces;
using HeartlandArtifact.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace HeartlandArtifact.ViewModels
{
    public class ForgotPasswordPageViewModel : ViewModelBase
    {
        private string _emailId;
        public string EmailId
        {
            get { return _emailId; }
            set { SetProperty(ref _emailId, value); }
        }
        public DelegateCommand ContinueBtnCommand { get; set; }
        public DelegateCommand GoBackCommand { get; set; }
        public ForgotPasswordPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ContinueBtnCommand = new DelegateCommand(GoToEnterOtpPage);
            GoBackCommand = new DelegateCommand(GoBack);
        }
        public async void GoBack()
        {
            //if (Device.RuntimePlatform == Device.Android)
            //{
            //    await NavigationService.GoBackAsync();
            //}
            if ((Device.RuntimePlatform == Device.iOS)|| Device.RuntimePlatform == Device.Android)
            {
                await NavigationService.NavigateAsync("SignInPage");
            }
        }
        public async void GoToEnterOtpPage()
        {
            var Toast = DependencyService.Get<IMessage>();
            try
            {
                if (string.IsNullOrEmpty(EmailId))
                {
                    Toast.LongAlert("Please enter you email address."); return;
                }
                if (!Regex.IsMatch(EmailId.Trim(), @"^((?:[a-zA-Z0-9]+)|(([a-zA-Z0-9]+(\.|\+|\-|_))+[a-zA-Z0-9]+))@(([a-zA-Z0-9]+(\.|\-))+[a-zA-Z]{2,4})$", RegexOptions.IgnoreCase))
                {
                    Toast.LongAlert("Oops, this email address doesn't look right."); return;
                }
                else
                {
                    IsBusy = true;
                    var response = await new ApiData().PostData<UserModel>("user/ForgotPassword?EmailId=" + EmailId + "&Otp=" + string.Empty, true);
                    if (response != null && response.data != null)
                    {
                        if (response.status == "Success")
                        {
                            Toast.LongAlert("We have sent an OTP to your entered email id. Please check your email inbox.");
                            var navigationParams = new NavigationParameters();
                            navigationParams.Add("FromForgetPassword", true);
                            navigationParams.Add("Email_Id", EmailId);
                            await NavigationService.NavigateAsync("EnterOtpPage", navigationParams);
                        }
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
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Email"))
            {
                EmailId = (string)parameters["Email"];
            }
        }
        public override void OnNavigatingTo(INavigationParameters parameters)
        {
        }
    }
}
