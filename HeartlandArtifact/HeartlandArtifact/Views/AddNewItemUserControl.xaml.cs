using HeartlandArtifact.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewItemUserControl : ContentView
    {          
        public AddNewItemUserControl()
        {
            InitializeComponent();
        }

        private void GoBack_Tapped(object sender, EventArgs e)
        {
            var veiwModel = BindingContext as HomePageViewModel;
            veiwModel.HomeIsVisible = true;
            veiwModel.AddNewItemUserControlIsVisible = false;
        }

        private void CollectionDropdown_Tapped(object sender, EventArgs e)
        {
            var viewModel = (BindingContext as HomePageViewModel);
            viewModel.CollectionDropdownIsVisible = !viewModel.CollectionDropdownIsVisible;
            viewModel.ContentViewIsVisible = !viewModel.ContentViewIsVisible;
        }
        private void CategoryDropdown_Tapped(object sender, EventArgs e)
        {
            var viewModel = (BindingContext as HomePageViewModel);
            viewModel.CategoryDropdownIsVisible = !viewModel.CategoryDropdownIsVisible;
            viewModel.ContentViewIsVisible = !viewModel.ContentViewIsVisible;
        }
        private void ContentView_Tapped(object sender, EventArgs e)
        {
            var viewModel = (BindingContext as HomePageViewModel);
            viewModel.CategoryDropdownIsVisible = false;
            viewModel.CollectionDropdownIsVisible = false;
            viewModel.ContentViewIsVisible = !viewModel.ContentViewIsVisible;
        }
    }
}