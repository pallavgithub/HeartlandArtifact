using Prism.Commands;
using Prism.Navigation;

namespace HeartlandArtifact.ViewModels
{
    public class HomeUserControlViewModel : ViewModelBase
    {
        public DelegateCommand MyProfileCommand{get;set;}
        public HomeUserControlViewModel(INavigationService navigationService) : base(navigationService)
        {
            MyProfileCommand = new DelegateCommand(GoToProfilePage);
        }
        public void GoToProfilePage()
        {
            NavigationService.NavigateAsync("ProfilePage");
        }
    }
}
