using Prism.Commands;
using Prism.Navigation;
namespace HeartlandArtifact.ViewModels
{
    public class EnterOtpPageViewModel : ViewModelBase
    {
        private bool _isFromForgotPassword;
        public bool IsFromForgotPassword
        {
            get { return _isFromForgotPassword; }
            set { SetProperty(ref _isFromForgotPassword, value); }
        }
        private string _text1;
        public string Text1
        {
            get { return _text1; }
            set { SetProperty(ref _text1, value); }
        }
        private string _text2;
        public string Text2
        {
            get { return _text2; }
            set { SetProperty(ref _text2, value); }
        }
        private string _text3;
        public string Text3
        {
            get { return _text3; }
            set { SetProperty(ref _text3, value); }
        }
        private string _text4;
        public string Text4
        {
            get { return _text4; }
            set { SetProperty(ref _text4, value); }
        }
        private string _timerText;
        public string TimerText
        {
            get { return _timerText; }
            set { SetProperty(ref _timerText, value); }
        }
        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand SubmitBtnCommand { get; set; }
        public EnterOtpPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            GoBackCommand = new DelegateCommand(GoBack);
            SubmitBtnCommand = new DelegateCommand(SubmitButtonClicked);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            IsFromForgotPassword = (bool)parameters["FromForgetPassword"];
        }
        public void GoBack()
        {
            NavigationService.GoBackAsync();
        }
        public void SubmitButtonClicked()
        {
            if (IsFromForgotPassword)
                NavigationService.NavigateAsync("ChangePasswordPage");
            else
                NavigationService.NavigateAsync("HomePage");
        }

    }
}
