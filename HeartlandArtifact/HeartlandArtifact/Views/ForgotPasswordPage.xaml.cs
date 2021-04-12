using HeartlandArtifact.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            (BindingContext as ForgotPasswordPageViewModel).GoBack();
            return true;
        }
    }
}