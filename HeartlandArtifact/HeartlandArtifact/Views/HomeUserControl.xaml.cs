using HeartlandArtifact.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeUserControl : ContentView
    {
        public HomeUserControl()
        {
            InitializeComponent();
        }

        private void MyCollection_Tapped(object sender, System.EventArgs e)
        {
            (BindingContext as HomePageViewModel).MyCollectionVisible = true;
            (BindingContext as HomePageViewModel).HomeIsVisible = false;

        } 
        private void MyProfile_Tapped(object sender, System.EventArgs e)
        {
            (BindingContext as HomePageViewModel).MyCollectionVisible = false;
            (BindingContext as HomePageViewModel).HomeIsVisible = false;
            //(BindingContext as HomePageViewModel)._nav.NavigateAsync("ProfilePage"); $"NavigationPage/{path}"
            (BindingContext as HomePageViewModel)._nav.NavigateAsync($"NavigationPage/ProfilePage"); 

        }
    }
}