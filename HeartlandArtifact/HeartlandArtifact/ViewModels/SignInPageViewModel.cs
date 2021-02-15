using Prism.Commands;
using Prism.Navigation;
namespace HeartlandArtifact.ViewModels
{
    class SignInPageViewModel : ViewModelBase
    {
        private bool _showPassword;
        public bool ShowPassword
        {
            get { return _showPassword; }
            set { SetProperty(ref _showPassword, value); }
        }
        public DelegateCommand GoToSignUpPageCommand { get; set; }
        public DelegateCommand GoToForgotPasswordPageCommand { get; set; }
        public SignInPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GoToSignUpPageCommand = new DelegateCommand(GoToSignUpPage);
            GoToForgotPasswordPageCommand = new DelegateCommand(GoToForgotPasswordPage);
        }
        public void GoToSignUpPage()
        {
            NavigationService.NavigateAsync("SignUpPage");
        }
        public void GoToForgotPasswordPage()
        {
            NavigationService.NavigateAsync("ForgotPasswordPage");
        }
    }
}
