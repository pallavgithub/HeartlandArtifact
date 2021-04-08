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
    }
}