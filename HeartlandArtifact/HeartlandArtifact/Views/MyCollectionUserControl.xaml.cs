using HeartlandArtifact.Models;
using HeartlandArtifact.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyCollectionUserControl : ContentView
    {
        public MyCollectionUserControl()
        {
            InitializeComponent();
        }

        private async void GoToCategoryList_Tapped(object sender, EventArgs e)
        {
            var selectedCollection = (((TappedEventArgs)e).Parameter) as CollectionModel;
            var viewModel = BindingContext as HomePageViewModel;
            await viewModel.GetUserCategories(selectedCollection);
            if (viewModel.IsEditIconVisible)
            {
                viewModel.IsEditIconVisible = false;
            }
            viewModel.CategoryUserControlIsVisible = true;
            viewModel.MyCollectionVisible = false;
        }
    }
}