using Prism.Commands;
using Prism.Navigation;

namespace HeartlandArtifact.ViewModels
{
    public class ForgotPasswordPageViewModel:ViewModelBase
    {
        public DelegateCommand ContinueBtnCommand { get; set; }
        public DelegateCommand GoBackCommand { get; set; }
        public ForgotPasswordPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ContinueBtnCommand = new DelegateCommand(GoToEnterOtpPage);
            GoBackCommand = new DelegateCommand(GoBack);
        }
        public void GoBack()
        {
            NavigationService.GoBackAsync();
        }
        public void GoToEnterOtpPage()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("FromForgetPassword", true);
            NavigationService.NavigateAsync("EnterOtpPage",navigationParams);
        }
    }
}
