using HeartlandArtifact.Services.Contracts;
using HeartlandArtifact.ViewModels;
using System;
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
            var MenuTabGesture = new TapGestureRecognizer();
            MenuTabGesture.Tapped += Menu_Tapped;
            MasterPage.Menu_Icon.GestureRecognizers.Add(MenuTabGesture);
            _facebookManager = DependencyService.Get<IFacebookManager>();
            _googleManager = DependencyService.Get<IGoogleManager>();
        }
        private void Logout_Tapped(object sender, EventArgs e)
        {
            (BindingContext as HomePageViewModel).LogoutPopupIsVisible = true;
            IsPresented = false;
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
            if (item.Title == "Home")
            {
                (BindingContext as HomePageViewModel).MyCollectionVisible = false;
                (BindingContext as HomePageViewModel).AddNewItemUserControlIsVisible = false;
                (BindingContext as HomePageViewModel).CategoryUserControlIsVisible = false;
                (BindingContext as HomePageViewModel).SoldItemsIsVisible = false;
                (BindingContext as HomePageViewModel).HomeIsVisible = true;
            }
            if (item.Title == "Sold Items")
            {
                //(BindingContext as HomePageViewModel).HomeIsVisible = false;
                //(BindingContext as HomePageViewModel).SoldItemsIsVisible = true;
            }
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
        private void Menu_Tapped(object sender, EventArgs e)
        {
            IsPresented = !IsPresented;
        }

        private void CancelLogout_Tapped(object sender, EventArgs e)
        {
            (BindingContext as HomePageViewModel).LogoutPopupIsVisible = false;
        }

        private async void ConfirmLogout_Tapped(object sender, EventArgs e)
        {
            _facebookManager.Logout();
            _googleManager.Logout();
            Application.Current.Properties["IsLogedIn"] = false;
            Application.Current.Properties["LogedInUserId"] = 0;
            Application.Current.Properties["UserName"] = string.Empty;
            await Application.Current.SavePropertiesAsync();
            (BindingContext as HomePageViewModel).LogoutPopupIsVisible = false;
            await (BindingContext as HomePageViewModel)._nav.NavigateAsync("/SignInPage");
        }
    }
}