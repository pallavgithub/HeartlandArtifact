using HeartlandArtifact.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SoldItemDetailsUserControl : ContentView
    {
        public SoldItemDetailsUserControl()
        {
            InitializeComponent();
        }

        private async void GoBack_Tapped(object sender, System.EventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            _vm.SoldItemDetailsUserControlIsVisible = false;
            if (_vm.GoBackFromSoldItemDetail == "SoldItems")
                _vm.SoldItemsIsVisible = true;
            if (_vm.GoBackFromSoldItemDetail == "ItemDetails")
            {
                await _vm.GetUserSoldItems();
                _vm.SoldItemsIsVisible = true;
            }
        }

        private async void AddAnotherItem_Tapped(object sender, System.EventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            _vm.GoBackFromAddItem = "SoldItemDetailsUserControl";
            await _vm.GetUserCollections();
            if (_vm.CollectionData != null)
            {
                _vm.CollectionIdForNewItem = _vm.CollectionData.CollectionId;
                _vm.CollectionNameForNewItem = _vm.CollectionData.CollectionName;
            }
            if (_vm.CategoryData != null)
            {
                _vm.CategoryIdForNewItem = _vm.CategoryData.CategoryId;
                _vm.CategoryNameForNewItem = _vm.CategoryData.CategoryName;
            }
            await _vm.GetAllUserCategories();
            _vm.SoldItemDetailsUserControlIsVisible = false;
            _vm.AddNewItemUserControlIsVisible = true;
        }
    }
}