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
    }
}