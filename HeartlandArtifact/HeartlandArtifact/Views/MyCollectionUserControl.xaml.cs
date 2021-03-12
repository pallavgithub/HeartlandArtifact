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

        private void GoToCategoryList_Tapped(object sender, EventArgs e)
        {
            var selectedCollection = (((TappedEventArgs)e).Parameter) as CollectionModel;
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.GetUserCategories(selectedCollection);
            viewModel.CategoryUserControlIsVisible = true;
            viewModel.MyCollectionVisible = false;
        }
    }
}