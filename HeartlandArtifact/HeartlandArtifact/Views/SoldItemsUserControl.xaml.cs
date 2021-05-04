using HeartlandArtifact.Models;
using HeartlandArtifact.ViewModels;
using System;
using System.Collections.ObjectModel;
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
                _vm.SearchBoxIsVisible = false;
                _vm.SearchText = string.Empty;
                _vm.SoldItemsIsVisible = false;
                _vm.HomeIsVisible = true;
            }
        }
        private async void SoldItem_Tapped(object sender, EventArgs e)
        {
            var selectedItem = ((TappedEventArgs)e).Parameter as MySoldItemModel;
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.SearchBoxIsVisible = false;
            viewModel.SearchText = string.Empty;
            viewModel.GoBackFromSoldItemDetail = "SoldItems";
            await viewModel.GetSoldItemDetailsById(selectedItem.Id);
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
        private void SearchBtn_Tapped(object sender, EventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            _vm.SearchBoxIsVisible = !_vm.SearchBoxIsVisible;
        }

        private void SearchItem(object sender, TextChangedEventArgs e)
        {
            try
            {
                var _vm = BindingContext as HomePageViewModel;
                var searchText = _vm.SearchText;
                _vm.AllSoldItems = new ObservableCollection<MySoldItemModel>();
                foreach (var item in _vm.SoldItems)
                {
                    if (item.Title.Contains(searchText))
                        _vm.AllSoldItems.Add(item);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CloseSearch_Tapped(object sender, EventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            _vm.AllSoldItems = new ObservableCollection<MySoldItemModel>(_vm.SoldItems);
            _vm.SearchBoxIsVisible = !_vm.SearchBoxIsVisible;
            _vm.SearchText = string.Empty;
        }
    }
}