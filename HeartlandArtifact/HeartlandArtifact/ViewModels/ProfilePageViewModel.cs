using Prism.Commands;
using Prism.Navigation;

namespace HeartlandArtifact.ViewModels
{
    public class ProfilePageViewModel:ViewModelBase
    {
        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand NextCommand { get; set; }
        public ProfilePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GoBackCommand = new DelegateCommand(GoBack);
            NextCommand = new DelegateCommand(Next);
        }
        public void GoBack()
        {
            NavigationService.NavigateAsync("/HomePage");
        }
        public void Next()
        {
            NavigationService.NavigateAsync("MyCollectionPage");
        }
    }
}
