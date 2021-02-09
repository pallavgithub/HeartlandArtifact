using Prism.Commands;
using Prism.Navigation;

namespace HeartlandArtifact.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
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
        public DelegateCommand GoBackToSignInPage { get; set; }
        public SignUpPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GoBackToSignInPage = new DelegateCommand(GoToSignInPage);
        }
        public void GoToSignInPage()
        {
            NavigationService.GoBackAsync();
        }
    }
}
