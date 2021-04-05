using HeartlandArtifact.ViewModels;
using System;
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

        private void AddItemBtn_Tapped(object sender, EventArgs e)
        {
            (BindingContext as HomePageViewModel).ItemsUserControlIsVisible = false;
            (BindingContext as HomePageViewModel).AddNewItemUserControlIsVisible = true;
        }

        private void GoBack_Tapped(object sender, EventArgs e)
        {
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.ItemsUserControlIsVisible = false;
            viewModel.HomeIsVisible = true;
        }

        private void DeleteItemButton_Tapped(object sender, EventArgs e)
        {
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.DeleteItemIconIsVisible = !viewModel.DeleteItemIconIsVisible;
        }
    }
}