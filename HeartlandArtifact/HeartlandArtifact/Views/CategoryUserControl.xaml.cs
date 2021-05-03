using HeartlandArtifact.Models;
using HeartlandArtifact.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryUserControl : ContentView
    {
        public CategoryUserControl()
        {
            InitializeComponent();
        }
        private void GoBack_Tapped(object sender, EventArgs e)
        {
            var viewModel = BindingContext as HomePageViewModel;
            if (viewModel.IsEditCategoryIconVisible)
            {
                viewModel.IsEditCategoryIconVisible = false;
            }
            else
            {
                viewModel.MyCollectionVisible = true;
                viewModel.CategoryUserControlIsVisible = false;
            }
        }
        private void AddNewCategory_Tapped(object sender, EventArgs e)
        {
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.AddCategoryPopupIsVisible = !viewModel.AddCategoryPopupIsVisible;
        }
        private void EditCategoryBtn_Tapped(object sender, EventArgs e)
        {
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.IsEditCategoryIconVisible = !viewModel.IsEditCategoryIconVisible;
        }
        private void EditIcon_Tapped(object sender, EventArgs e)
        {
            var selectedCategory = ((TappedEventArgs)e).Parameter as CategoryModel;
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.NewCategoryName = selectedCategory.CategoryName;
            viewModel.CategoryData = new CategoryModel();
            viewModel.CategoryData = selectedCategory;
            viewModel.EditCategoryPopupIsVisible = !viewModel.EditCategoryPopupIsVisible;
        }
        private void DeleteIcon_Tapped(object sender, EventArgs e)
        {
            var selectedCategory = ((TappedEventArgs)e).Parameter as CategoryModel;
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.CategoryData = new CategoryModel();
            viewModel.CategoryData = selectedCategory;
            viewModel.DeleteCategoryPopupIsVisible = !viewModel.DeleteCategoryPopupIsVisible;
        }
        private async void GoToItemsList_Tapped(object sender, EventArgs e)
        {
            var selectedCategory = ((TappedEventArgs)e).Parameter as CategoryModel;
            var viewModel = BindingContext as HomePageViewModel;
            await viewModel.GetUserItems(selectedCategory);
            if (viewModel.IsEditCategoryIconVisible)
            {
                viewModel.IsEditCategoryIconVisible = false;
            }
            viewModel.ItemsUserControlIsVisible = true;
            viewModel.CategoryUserControlIsVisible = false;
        }
    }
}