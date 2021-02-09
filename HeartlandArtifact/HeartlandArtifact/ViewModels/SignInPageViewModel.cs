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
        public SignInPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GoToSignUpPageCommand = new DelegateCommand(GoToSignUpPage);
        }
        public void GoToSignUpPage()
        {
            NavigationService.NavigateAsync("SignUpPage");
        }
    }
}
