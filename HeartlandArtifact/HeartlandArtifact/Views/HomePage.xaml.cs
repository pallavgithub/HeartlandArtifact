using HeartlandArtifact.Interfaces;
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
        long lastPress;
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
        protected override bool OnBackButtonPressed()
        {
            var _vm = BindingContext as HomePageViewModel;
            if (_vm.HomeIsVisible)
            {
                var Toast = DependencyService.Get<IMessage>();
                long currentTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
                if (currentTime - lastPress > 3000)
                {
                    Toast.ShortAlert("Press again to exit.");
                    lastPress = currentTime;
                }
                else
                {
                    return false;
                }
            }
            if (_vm.MyCollectionVisible)
            {
                _vm.DeleteCollectionPopupIsVisible = false;
                _vm.AddCollectionPopupIsVisible = false;
                _vm.EditCollectionPopupIsVisible = false;
                _vm.IsEditIconVisible = false;
                _vm.MyCollectionVisible = false;
                _vm.HomeIsVisible = true;
            }
            if (_vm.CategoryUserControlIsVisible)
            {
                _vm.DeleteCategoryPopupIsVisible = false;
                _vm.AddCategoryPopupIsVisible = false;
                _vm.EditCategoryPopupIsVisible = false;
                _vm.IsEditCategoryIconVisible = false;
                _vm.CategoryUserControlIsVisible = false;
                _vm.MyCollectionVisible = true;
            }
            if (_vm.ItemsUserControlIsVisible)
            {
                _vm.DeleteItemIconIsVisible = false;
                _vm.DeleteItemPopupIsVisible = false;
                _vm.ItemsUserControlIsVisible = false;
                _vm.CategoryUserControlIsVisible = true;
            }
            if (_vm.ItemDetailsUserControlIsVisible)
            {
                if (_vm.MarkAsSoldDetailsIsVisible)
                {
                    _vm.MarkAsSoldDetailsIsVisible = false;
                }
                if (_vm.AllItems == null)
                {
                    _vm.ItemDetailsUserControlIsVisible = false;
                    _vm.HomeIsVisible = true;
                }
                else
                {
                    _vm.ItemDetailsUserControlIsVisible = false;
                    _vm.ItemsUserControlIsVisible = true;
                }
            }
            if (_vm.SoldItemDetailsUserControlIsVisible)
            {
                _vm.SoldItemDetailsUserControlIsVisible = false;
                if (_vm.GoBackFromSoldItemDetail == "SoldItems")
                    _vm.SoldItemsIsVisible = true;
                if (_vm.GoBackFromSoldItemDetail == "ItemDetails")
                    _vm.ItemDetailsUserControlIsVisible = true;
            }
            if (_vm.AddNewItemUserControlIsVisible)
            {
                if (_vm.AddMultipleItemPhotosIsVisible)
                {
                    _vm.AddMultipleItemPhotosIsVisible = false;
                }
                else
                {
                    _vm.EmptyAddItemForm();
                    _vm.AddNewItemUserControlIsVisible = false;
                    if (_vm.GoBackFromAddItem == "ItemDetailsUserControl")
                    {
                        _vm.ItemDetailsUserControlIsVisible = true;
                    }
                    else if (_vm.GoBackFromAddItem == "ItemUserControl")
                    {
                        _vm.ItemsUserControlIsVisible = true;
                    }
                    else if (_vm.GoBackFromAddItem == "HomeUserControl")
                    {
                        _vm.HomeIsVisible = true;
                    }
                    else if (_vm.GoBackFromAddItem == "EditItem")
                    {
                        _vm.ItemDetailsUserControlIsVisible = true;
                    }
                }
            }
            if (_vm.SoldItemsIsVisible)
            {
                if (_vm.DeleteSoldItemIconIsVisible)
                    _vm.DeleteSoldItemIconIsVisible = false;
                else
                {
                    _vm.SoldItemsIsVisible = false;
                    _vm.HomeIsVisible = true;
                }
            }

            return true;
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            var item = e.SelectedItem as HomePageMasterMenuItem;
            if (item == null)
                return;

            // var page = (Page)Activator.CreateInstance(item.TargetType);
            // page.Title = item.Title;

            // Detail = new NavigationPage(page);
            _vm.MyCollectionVisible = false;
            _vm.AddNewItemUserControlIsVisible = false;
            _vm.CategoryUserControlIsVisible = false;
            _vm.ItemDetailsUserControlIsVisible = false;
            _vm.ItemsUserControlIsVisible = false;
            _vm.SoldItemDetailsUserControlIsVisible = false;
            if (item.Title == "Home")
            {
                _vm.SoldItemsIsVisible = false;
                _vm.HomeIsVisible = true;
            }
            if (item.Title == "Sold Items")
            {
                _vm.HomeIsVisible = false;
                _vm.SoldItemsIsVisible = true;
                _vm.GetUserSoldItems();
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
            IsBusy = true;
            (BindingContext as HomePageViewModel).LogoutPopupIsVisible = false;
            _facebookManager.Logout();
            _googleManager.Logout();
            Application.Current.Properties["IsLogedIn"] = false;
            Application.Current.Properties["LogedInUserId"] = 0;
            Application.Current.Properties["UserName"] = string.Empty;
            await Application.Current.SavePropertiesAsync();
            await (BindingContext as HomePageViewModel)._nav.NavigateAsync("SignInPage");
            IsBusy = false;
        }
    }
}