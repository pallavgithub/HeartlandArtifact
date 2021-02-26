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
        public void GoBack()
        {
            NavigationService.GoBackAsync();
        }
        public async void GoToEnterOtpPage()
        {
            var Toast = DependencyService.Get<IMessage>();
            try
            {
                if (string.IsNullOrEmpty(EmailId))
                {
                    Toast.LongAlert("Email is required."); return;
                }
                if (!Regex.IsMatch(EmailId.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    Toast.LongAlert("Invalid email address."); return;
                }
                else
                {
                    IsBusy = true;
                    var response = await new ApiData().PostData<UserModel>("user/ForgotPassword?EmailId=" + EmailId + "&Otp=" + string.Empty, true);
                    if (response != null && response.data != null)
                    {
                        if (response.status == "Success")
                        {
                            Toast.LongAlert("Otp sent.");
                            var navigationParams = new NavigationParameters();
                            navigationParams.Add("FromForgetPassword", true);
                            navigationParams.Add("Email_Id", EmailId);
                            await NavigationService.NavigateAsync("EnterOtpPage", navigationParams);
                        }
                    }
                    IsBusy = false;
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
