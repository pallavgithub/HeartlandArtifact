using Prism.Navigation;

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
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            HomeIsVisible = true;
        }
    }
}
