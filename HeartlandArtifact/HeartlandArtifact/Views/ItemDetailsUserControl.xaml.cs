﻿using HeartlandArtifact.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailsUserControl : ContentView
    {
        public ItemDetailsUserControl()
        {
            InitializeComponent();
        }

        private void GoBack_Tapped(object sender, EventArgs e)
        {
            var viewModel = BindingContext as HomePageViewModel;
            if (viewModel.MarkAsSoldDetailsIsVisible)
            {
                viewModel.MarkAsSoldDetailsIsVisible = false;
            }
            else
            {
                viewModel.DeleteItemIconIsVisible = false;
                viewModel.ItemDetailsUserControlIsVisible = false;
                viewModel.ItemsUserControlIsVisible = true;
            }
        }

        private void MarkAsSoldBtn_Tapped(object sender, EventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            if (_vm.MarkAsSoldDetailsIsVisible)
            {
                _vm.ItemMarkAsSold();
            }
            else
            {
                _vm.MarkAsSoldDetailsIsVisible = true;
            }
        }

        private void DatePicker_Tapped(object sender, EventArgs e)
        {
            Date_Picker.IsVisible = true;
            Date_Picker.Focus();
        }

        private void Date_Picker_Unfocused(object sender, FocusEventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            if (Date_Picker.Date != null)
            {
                _vm.SoldDate = Date_Picker.Date.ToString("MM-dd-yyyy");
                _vm.DateLabelIsVisible = true;
            }
            Date_Picker.IsVisible = false;
        }

        private void Date_Picker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            _vm.SoldDate = Date_Picker.Date.ToString("MM-dd-yyyy");
            _vm.DateLabelIsVisible = true;
        }

        private void AddAnotherItemBtn_Tapped(object sender, EventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            _vm.ItemDetailsUserControlIsVisible = false;
            _vm.AddNewItemUserControlIsVisible = true;
        }
    }
}