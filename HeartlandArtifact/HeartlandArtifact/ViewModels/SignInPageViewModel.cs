using HeartlandArtifact.Models;
using HeartlandArtifact.Services.Contracts;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace HeartlandArtifact.ViewModels
{
    class SignInPageViewModel : ViewModelBase
    {
        private readonly IGoogleManager _googleManager;
        private readonly IFacebookManager _facebookManager;
        public DelegateCommand FacebookLoginCommand { get; set; }
        public DelegateCommand FacebookLogoutCommand { get; set; }
        public DelegateCommand GoogleLoginCommand { get; set; }
        public DelegateCommand GoogleLogoutCommand { get; set; }
        public DelegateCommand GoToSignUpPageCommand { get; set; }
        public DelegateCommand GoToForgotPasswordPageCommand { get; set; }
        private FacebookUser _facebookUser;
        private GoogleUser _googleUser;
        private bool _isLogedIn;
        private bool _showPassword;

        public bool ShowPassword
        {
            get { return _showPassword; }
            set { SetProperty(ref _showPassword, value); }
        }
        public FacebookUser FacebookUser
        {
            get { return _facebookUser; }
            set { SetProperty(ref _facebookUser, value); }
        }

        public GoogleUser GoogleUser
        {
            get { return _googleUser; }
            set { SetProperty(ref _googleUser, value); }
        }

        public bool IsLogedIn
        {
            get { return _isLogedIn; }
            set { SetProperty(ref _isLogedIn, value); }
        }

        public SignInPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _facebookManager = DependencyService.Get<IFacebookManager>();
            _googleManager = DependencyService.Get<IGoogleManager>();

            IsLogedIn = false;
            FacebookLoginCommand = new DelegateCommand(FacebookLogin);
            GoogleLoginCommand = new DelegateCommand(GoogleLogin);
            FacebookLogoutCommand = new DelegateCommand(FacebookLogout);
            GoogleLogoutCommand = new DelegateCommand(GoogleLogout);
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

        private void FacebookLogout()
        {
            _facebookManager.Logout();
            IsLogedIn = false;
        }

        private void FacebookLogin()
        {
            _facebookManager.Login(OnFacebookLoginComplete);
        }

        private void GoogleLogout()
        {
            _googleManager.Logout();
            IsLogedIn = false;
        }

        private void GoogleLogin()
        {
            _googleManager.Login(OnGoogleLoginComplete);

        }
        private void OnFacebookLoginComplete(FacebookUser facebookUser, string message)
        {
            if (facebookUser != null)
            {
                FacebookUser = facebookUser;
                IsLogedIn = true;
            }
            else
            {
                //_dialogService.DisplayAlertAsync("Error", message, "Ok");
            }
        }

        private void OnGoogleLoginComplete(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                GoogleUser = googleUser;
                IsLogedIn = true;
            }
            else
            {
                //_dialogService.DisplayAlertAsync("Error", message, "Ok");
            }
        }
    }
}
