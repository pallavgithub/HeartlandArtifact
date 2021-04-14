using HeartlandArtifact.ViewModels;
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
            if (viewModel.AllItems == null)
            {
                viewModel.ItemDetailsUserControlIsVisible = false;
                viewModel.HomeIsVisible = true;
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
                _vm.ItemSoldDate = Date_Picker.Date.ToString("MM-dd-yyyy");
                _vm.DateLabelIsVisible = true;
            }
            Date_Picker.IsVisible = false;
        }

        private void Date_Picker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            _vm.ItemSoldDate = Date_Picker.Date.ToString("MM-dd-yyyy");
            _vm.DateLabelIsVisible = true;
        }

        private void AddAnotherItemBtn_Tapped(object sender, EventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            _vm.GoBackFromAddItem = "ItemDetailsUserControl";
            _vm.ItemDetailsUserControlIsVisible = false;
            _vm.AddNewItemUserControlIsVisible = true;
        }
    }
}