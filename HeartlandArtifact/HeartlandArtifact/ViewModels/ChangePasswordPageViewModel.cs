using Prism.Commands;
using Prism.Navigation;
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
        public void SetNewPassword()
        {
            NavigationService.NavigateAsync("SignInPage");
        }
    }
}
