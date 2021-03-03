using HeartlandArtifact.Services.Contracts;
using HeartlandArtifact.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : MasterDetailPage
    {
        private readonly IGoogleManager _googleManager;
        private readonly IFacebookManager _facebookManager;
        public HomePage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += Logout_Tapped;
            MasterPage.Logout_Option.GestureRecognizers.Add(tapGestureRecognizer);
            _facebookManager = DependencyService.Get<IFacebookManager>();
            _googleManager = DependencyService.Get<IGoogleManager>();
        }
        private async void Logout_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            _facebookManager.Logout();
            _googleManager.Logout();
            Application.Current.Properties["IsLogedIn"] = false;
            await Application.Current.SavePropertiesAsync();
            //App.Current.MainPage = new NavigationPage(new SignInPage());
            await (BindingContext as HomePageViewModel)._nav.NavigateAsync("/SignInPage");
        }
        protected override bool OnBackButtonPressed() => true;


        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as HomePageMasterMenuItem;
            if (item == null)
                return;

            // var page = (Page)Activator.CreateInstance(item.TargetType);
            // page.Title = item.Title;

            // Detail = new NavigationPage(page);
            if(item.Title=="Home")
            {
                (BindingContext as HomePageViewModel).SoldItemsIsVisible = false;
                (BindingContext as HomePageViewModel).HomeIsVisible = true;
            }
            if (item.Title == "Sold Items")
            {
                (BindingContext as HomePageViewModel).HomeIsVisible = false;
                (BindingContext as HomePageViewModel).SoldItemsIsVisible = true;
            }
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
        private void Menu_Tapped(object sender, EventArgs e)
        {
            IsPresented = true;
        }
    }
}