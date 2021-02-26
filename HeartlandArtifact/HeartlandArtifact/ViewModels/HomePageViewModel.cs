using HeartlandArtifact.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace HeartlandArtifact.ViewModels
{
    public class HomePageViewModel:ViewModelBase
    {
        private bool _soldItemsIsVisible;
        public bool SoldItemsIsVisible
        {
            get { return _soldItemsIsVisible; }
            set { SetProperty(ref _soldItemsIsVisible, value); }
        }
        private bool _homeIsVisible;
        public bool HomeIsVisible
        {
            get { return _homeIsVisible; }
            set { SetProperty(ref _homeIsVisible, value); }
        }
        public DelegateCommand LogoutCommand { get; set; }
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            HomeIsVisible = true;
            LogoutCommand = new DelegateCommand(Logout);
        }
        public void Logout()
        {
            Application.Current.Properties["IsLogedIn"] = false;
            App.Current.MainPage = new SignInPage();
        }
    }
}
