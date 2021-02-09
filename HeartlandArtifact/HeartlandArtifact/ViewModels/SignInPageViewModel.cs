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
        public SignInPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
    }
}
