using Facebook.CoreKit;
using Foundation;
using Google.SignIn;
using HeartlandArtifact.Services.Contracts;
using Prism;
using Prism.Ioc;
using UIKit;
using Xamarin.Forms;

namespace HeartlandArtifact.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            DependencyService.Register<IFacebookManager, FacebookManager>();

            DependencyService.Register<IGoogleManager, GoogleManager>();
            // var googleServiceDictionary = NSDictionary.FromFile("GoogleService-Info.plist");
            // SignIn.SharedInstance.ClientId = googleServiceDictionary["CLIENT_ID"].ToString();


            SignIn.SharedInstance.ClientId = @"554175844065-dpgmkp1fmtldk9afno3gc2i158tr9b0s.apps.googleusercontent.com";
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
            AppEvents.ActivateApp();
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            //return base.OpenUrl(application, url, sourceApplication, annotation);

            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
