using HeartlandArtifact.Models;
using HeartlandArtifact.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SoldItemsUserControl : ContentView
    {
        public SoldItemsUserControl()
        {
            InitializeComponent();
        }

        private void DeleteSoldItemBtn_Tapped(object sender, EventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            _vm.DeleteSoldItemIconIsVisible = true;
        }
        private void GoBack_Tapped(object sender, EventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            if (_vm.DeleteSoldItemIconIsVisible)
            {
                _vm.DeleteSoldItemIconIsVisible = false;
            }
            else
            {
                _vm.SoldItemsIsVisible = false;
                _vm.HomeIsVisible = true;
            }
        }
        private void SoldItem_Tapped(object sender, EventArgs e)
        {
            var selectedItem = ((TappedEventArgs)e).Parameter as MySoldItemModel;
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.GoBackFromSoldItemDetail = "SoldItems";
            viewModel.GetSoldItemDetailsById(selectedItem.Id);
            if (viewModel.DeleteSoldItemIconIsVisible)
            {
                viewModel.DeleteSoldItemIconIsVisible = false;
            }
            viewModel.SoldItemDetailsUserControlIsVisible = true;
            viewModel.SoldItemsIsVisible = false;
        }
        private void DeleteIcon_Tapped(object sender, EventArgs e)
        {
            var selectedItem = ((TappedEventArgs)e).Parameter as MySoldItemModel;
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.SoldItemData = new MySoldItemModel();
            viewModel.SoldItemData = selectedItem;
            viewModel.DeleteSoldItemPopupIsVisible = true;
        }
    }
}