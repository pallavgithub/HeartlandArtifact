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
        public ChangePasswordPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            SetPasswordBtnCommand = new DelegateCommand(SetNewPassword);
        }
        public async void SetNewPassword()
        {
            var Toast = DependencyService.Get<IMessage>();
            if (string.IsNullOrEmpty(NewPassword) && string.IsNullOrEmpty(ConfirmNewPassword))
            {
                Toast.LongAlert("All fields are required"); return;
            }
            if (string.IsNullOrEmpty(NewPassword))
            {
                Toast.LongAlert("New password is required"); return;
            }
            if (NewPassword.Trim().Length < 8)
            {
                Toast.LongAlert("New Password must be at least 8 characters, no more than 15 characters."); return;
            }
            if (!Regex.IsMatch(NewPassword, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", RegexOptions.None))
            {
                Toast.LongAlert("Password must include at least one uppercase letter, one lowercase letter, one numeric digit, one special character."); return;
            }
            if (string.IsNullOrEmpty(ConfirmNewPassword))
            {
                Toast.LongAlert("Confirm New password is required"); return;
            }
            if (NewPassword != ConfirmNewPassword)
            {
                Toast.LongAlert("Confirm New password does not match"); return;
            }
            else
            {
                IsBusy = true;
                var response = await new ApiData().PostData<UserModel>("user/ResetPassword?Password=" + NewPassword, true);
                if (response != null && response.data != null&&response.status=="Success")
                {
                    Toast.LongAlert("Password has been updated successfully");
                    await NavigationService.NavigateAsync("SignInPage");
                }
                else
                {
                    Toast.LongAlert(response.message);

                }

                IsBusy = false;

            }
        }
    }
}
