using HeartlandArtifact.Models;
using HeartlandArtifact.ViewModels;
using HeartlandArtifact.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using static HeartlandArtifact.Views.HomePageMaster;

namespace HeartlandArtifact
{
    public partial class App
    {
        public static UserModel SignUpDetails { get; set; }
        public static UserDataModel User { get; set; }
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            App.User = new UserDataModel();
            App.SignUpDetails = new UserModel();
            if (Application.Current.Properties != null)
            {
               // await NavigationService.NavigateAsync("NavigationPage/SignInPage");
                if (Application.Current.Properties.ContainsKey("IsLogedIn"))
                {
                    if ((bool)Application.Current.Properties["IsLogedIn"] == true)
                    {
                        await NavigationService.NavigateAsync("NavigationPage/HomePage");
                    }
                    else
                    {
                        await NavigationService.NavigateAsync("NavigationPage/SignInPage");
                    }
                }
                else
                {
                    await NavigationService.NavigateAsync("NavigationPage/SignInPage");
                }
            }

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<ForgotPasswordPage, ForgotPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<EnterOtpPage, EnterOtpPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
           // containerRegistry.RegisterForNavigation<HomePageMaster, HomePageMasterViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
        }
    }
}
