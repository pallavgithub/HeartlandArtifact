using HeartlandArtifact.Helpers;
using HeartlandArtifact.Interfaces;
using HeartlandArtifact.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace HeartlandArtifact.ViewModels
{
    public class ChangePasswordPageViewModel : ViewModelBase
    {
        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
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
        private string _newPassword;
        public string NewPassword
        {
            get { return _newPassword; }
            set { SetProperty(ref _newPassword, value); }
        }
        private string _confirmNewPassword;
        public string ConfirmNewPassword
        {
            get { return _confirmNewPassword; }
            set { SetProperty(ref _confirmNewPassword, value); }
        }
        public DelegateCommand SetPasswordBtnCommand { get; set; }
        public DelegateCommand GoBackCommand { get; set; }
        public ChangePasswordPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            SetPasswordBtnCommand = new DelegateCommand(SetNewPassword);
            GoBackCommand = new DelegateCommand(GoBack);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("EmailId"))
            {
                Email = parameters["EmailId"].ToString();
            }
        }
        public async void SetNewPassword()
        {
            var Toast = DependencyService.Get<IMessage>();
            //if (string.IsNullOrEmpty(NewPassword) && string.IsNullOrEmpty(ConfirmNewPassword))
            //{
            //    Toast.LongAlert("All fields are mandatory."); return;
            //}
            if (string.IsNullOrEmpty(NewPassword))
            {
                Toast.LongAlert("Please enter your password."); return;
            }
            if (!Regex.IsMatch(NewPassword, @"^[a-zA-Z]{8,15}$", RegexOptions.None) || NewPassword.Trim().Length < 8)
            {
                Toast.LongAlert("Password must be between 8 to 15 characters, including uppercase, lowercase letters."); return;
            }
            if (string.IsNullOrEmpty(ConfirmNewPassword))
            {
                Toast.LongAlert("Please re-enter your password."); return;
            }
            if (NewPassword != ConfirmNewPassword)
            {
                Toast.LongAlert("Hey, confirm password should be same as the new password."); return;
            }
            else
            {
                IsBusy = true;
                UserModel model = new UserModel()
                {
                    EmailId = Email,
                    Password = NewPassword
                };
                var response = await new ApiData().PostData<UserModel>("user/ResetPassword", model, true);
                if (response != null && response.status == "Success")
                {
                    Toast.LongAlert("Password update successful!");
                    await NavigationService.NavigateAsync("SignInPage");
                }
                else
                {
                    Toast.LongAlert(response.message);
                }

                IsBusy = false;
            }
        }
        public async void GoBack()
        {
            //var navigationParams = new NavigationParameters();
            //navigationParams.Add("FromResetPassword", true);
            //NavigationService.GoBackAsync(navigationParams);

           
            if (Device.RuntimePlatform == Device.Android)
            {
                await NavigationService.GoBackAsync();
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("Email", Email);
                await NavigationService.NavigateAsync("ForgotPasswordPage", navigationParams);
            }
        }
    }
}
