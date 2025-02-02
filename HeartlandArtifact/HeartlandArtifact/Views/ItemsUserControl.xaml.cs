﻿using HeartlandArtifact.Models;
using HeartlandArtifact.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsUserControl : ContentView
    {
        public ItemsUserControl()
        {
            InitializeComponent();
        }

        private async void AddItemBtn_Tapped(object sender, EventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            _vm.GoBackFromAddItem = "ItemUserControl";
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
            _vm.ItemsUserControlIsVisible = false;
            _vm.AddNewItemUserControlIsVisible = true;
        }

        private void GoBack_Tapped(object sender, EventArgs e)
        {
            var viewModel = BindingContext as HomePageViewModel;
            if (viewModel.DeleteItemIconIsVisible)
            {
                viewModel.DeleteItemIconIsVisible = false;
            }
            else
            {
                viewModel.SearchBoxIsVisible = false;
                viewModel.SearchText = string.Empty;
                viewModel.ItemsUserControlIsVisible = false;
                viewModel.CategoryUserControlIsVisible = true;
            }
        }

        private void DeleteItemButton_Tapped(object sender, EventArgs e)
        {
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.DeleteItemIconIsVisible = !viewModel.DeleteItemIconIsVisible;
        }

        private async void GoToItemDetails_Tapped(object sender, EventArgs e)
        {
            var selectedItem = ((TappedEventArgs)e).Parameter as MyItemModel;
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.SearchBoxIsVisible = false;
            viewModel.SearchText = string.Empty;
            await viewModel.GetItemDetailsById(selectedItem.ItemId);
            if (viewModel.DeleteItemIconIsVisible)
            {
                viewModel.DeleteItemIconIsVisible = false;
            }
            viewModel.ItemDetailsUserControlIsVisible = true;
            viewModel.ItemsUserControlIsVisible = false;
        }

        private void DeleteIcon_Tapped(object sender, EventArgs e)
        {
            var selectedItem = ((TappedEventArgs)e).Parameter as MyItemModel;
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.ItemData = new MyItemModel();
            viewModel.ItemData = selectedItem;
            viewModel.DeleteItemPopupIsVisible = !viewModel.DeleteItemPopupIsVisible;
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
                _vm.AllItems = new ObservableCollection<MyItemModel>();
                foreach (var item in _vm.Items)
                {
                    if (item.Title.Contains(searchText))
                        _vm.AllItems.Add(item);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CloseSearch_Tapped(object sender, EventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            _vm.AllItems = new ObservableCollection<MyItemModel>(_vm.Items);
            _vm.SearchBoxIsVisible = !_vm.SearchBoxIsVisible;
            _vm.SearchText = string.Empty;
        }
    }
}