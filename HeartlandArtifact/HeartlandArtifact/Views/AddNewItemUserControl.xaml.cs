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
            if (viewModel.AddMultipleItemPhotosIsVisible)
            {
                viewModel.AddMultipleItemPhotosIsVisible = false;
            }
            else
            {
                viewModel.CollectionNameForNewItem = string.Empty;
                viewModel.CategoryNameForNewItem = string.Empty;
                viewModel.Title = string.Empty;
                viewModel.Material = string.Empty;
                viewModel.PerceivedValue = string.Empty;
                viewModel.Cost = string.Empty;
                viewModel.FoundBy = string.Empty;
                viewModel.ExCollection = string.Empty;
                viewModel.Length = string.Empty;
                viewModel.Country = string.Empty;
                viewModel.State = string.Empty;
                viewModel.Notes = string.Empty;
                pic.Source = string.Empty;
                viewModel.AddNewItemUserControlIsVisible = false;
                viewModel.ItemsUserControlIsVisible = true;
            }
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
            catch (Exception ex)
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
            catch (Exception ex)
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
                if (viewModel.AllUserCategories != null && viewModel.AllUserCategories.Count > 0)
                {
                    foreach (var item in viewModel.AllUserCategories)
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
        private void AddMultiplePhotos_Tapped(object sender, EventArgs e)
        {
            var ViewModel = BindingContext as HomePageViewModel;
            var Toast = DependencyService.Get<IMessage>();
            ViewModel.AddMultipleItemPhotosIsVisible = true;           
        }
        private async void AddNewPhoto_Tapped(object sender, EventArgs e)
        {
            try
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
                ((sender as Grid).Children[1] as Image).Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
                var st = file.GetStream();
                FileStream fs = st as FileStream;
                pic.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
               
                ViewModel.ImageStream = st;
                byte[] byteArray = ConvertToByteArrayFromStream();
                string base64Img = Convert.ToBase64String(byteArray);
                ViewModel.Base64ItemImagesList.Add(base64Img);
            }
            catch (Exception ex)
            {

            }
        }
        private byte[] ConvertToByteArrayFromStream()
        {
            var ViewModel = BindingContext as HomePageViewModel;
            byte[] byteArray = null;
            using (MemoryStream ms = new MemoryStream())
            {
                ViewModel.ImageStream.CopyTo(ms);
                byteArray = ms.ToArray();
            }
            return byteArray;
        }
        private void SaveBtn_Tapped(object sender, EventArgs e)
        {
            var Toast = DependencyService.Get<IMessage>();
            var _vm = BindingContext as HomePageViewModel;
            if (_vm.AddMultipleItemPhotosIsVisible)
            {
                if (_vm.Base64ItemImagesList.Count == 0)
                {
                    Toast.LongAlert("Please add atleast one image."); return;
                }
                else
                {
                    _vm.AddMultipleItemPhotosIsVisible = false;
                }
            }
            else
            {
                if (_vm.CollectionIdForNewItem == 0)
                {
                    Toast.LongAlert("Please select collection name."); return;
                }
                if (_vm.CategoryIdForNewItem == 0)
                {
                    Toast.LongAlert("Please select category name."); return;
                }
                if (string.IsNullOrEmpty(_vm.Title))
                {
                    Toast.LongAlert("Please enter Title."); return;
                }
                if (_vm.Base64ItemImagesList.Count == 0)
                {
                    Toast.LongAlert("Please add atleast one image."); return;
                }
                else
                {                   
                    _vm.AddNewItem();
                }
            }
        }
    }
}
