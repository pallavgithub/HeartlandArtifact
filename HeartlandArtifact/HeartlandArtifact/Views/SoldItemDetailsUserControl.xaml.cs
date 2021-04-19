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

        private void GoBack_Tapped(object sender, System.EventArgs e)
        {
            var _vm = BindingContext as HomePageViewModel;
            _vm.SoldItemDetailsUserControlIsVisible = false;
            if (_vm.GoBackFromSoldItemDetail == "SoldItems")
                _vm.SoldItemsIsVisible = true;
            if (_vm.GoBackFromSoldItemDetail == "ItemDetails")
                _vm.ItemDetailsUserControlIsVisible = true;
        }
    }
}