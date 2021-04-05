using HeartlandArtifact.Interfaces;
using HeartlandArtifact.Models;
using HeartlandArtifact.ViewModels;
using Plugin.Media;
using System;
using System.Collections.ObjectModel;
using System.IO;
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
            var viewModel = BindingContext as HomePageViewModel;
            viewModel.AddNewItemUserControlIsVisible = false;
            viewModel.ItemsUserControlIsVisible = true;
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

        private void CollectionDropdownRow_Tapped(object sender, EventArgs e)
        {
            try
            {
                var selectedCollection = ((TappedEventArgs)e).Parameter as CollectionModel;
                (BindingContext as HomePageViewModel).CollectionIdForNewItem = selectedCollection.CollectionId;
                (BindingContext as HomePageViewModel).CollectionNameForNewItem = selectedCollection.CollectionName;
                (BindingContext as HomePageViewModel).CollectionDropdownIsVisible = false;
                GetListForDropdown(selectedCollection);
            }
            catch(Exception ex)
            {

            }
        }
        private void CategoryDropdownRow_Tapped(object sender, EventArgs e)
        {
            try
            {
                var selectedcategory = ((TappedEventArgs)e).Parameter as CategoryModel;
                (BindingContext as HomePageViewModel).CategoryIdForNewItem = selectedcategory.CategoryId;
                (BindingContext as HomePageViewModel).CategoryNameForNewItem = selectedcategory.CategoryName;
                (BindingContext as HomePageViewModel).CategoryDropdownIsVisible = false;
            }
            catch(Exception ex)
            {

            }
           // (BindingContext as HomePageViewModel).GetListForDropdown(selectedCollection);
        }
        public void GetListForDropdown(CollectionModel selectedCollection)
        {
            var viewModel = BindingContext as HomePageViewModel;
            try
            {
                viewModel.CategoryList = new ObservableCollection<CategoryModel>();
                if (viewModel.AllCategories != null && viewModel.AllCategories.Count > 0)
                {
                    foreach (var item in viewModel.AllCategories)
                    {
                        if (item.CollectionId == selectedCollection.CollectionId)
                            viewModel.CategoryList.Add(item);
                    }
                    viewModel.CategoryList = new ObservableCollection<CategoryModel>(viewModel.CategoryList);
                }

                //CategoryDropdownList.ItemsSource = viewModel.CategoryList;
            }
            catch (Exception e)
            {

            }
        }

        private async void AddNewPhoto_Tapped(object sender, EventArgs e)
        {
            var ViewModel = BindingContext as HomePageViewModel;
            var Toast = DependencyService.Get<IMessage>();
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //DisplayAlert("No Camera", ":( No camera available.", "OK");
                Toast.LongAlert("No Camera available");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
            });
            if (file == null)
                return;
            pic.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            var st = file.GetStream();
            FileStream fs = st as FileStream;
            ViewModel.ImageStream = st;
            //await ViewModel.UploadImageAsync(st, fs.Name);
        }
    }
}