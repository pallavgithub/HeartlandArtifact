using HeartlandArtifact.Interfaces;
using HeartlandArtifact.Models;
using HeartlandArtifact.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
            MessagingCenter.Subscribe<string>(this, "SetImageSources", async (message) =>
            {
                var viewModel = BindingContext as HomePageViewModel;
                viewModel.newItemImage = pic;
                viewModel.newItemImage_one = pic1;
                viewModel.newItemImage_two = pic2;
                viewModel.newItemImage_three = pic3;
            });
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
                cross1.IsVisible = false;
                cross2.IsVisible = false;
                cross3.IsVisible = false;
                viewModel.EmptyAddItemForm();
                viewModel.AddNewItemUserControlIsVisible = false;
                if (viewModel.GoBackFromAddItem == "ItemDetailsUserControl")
                {
                    viewModel.ItemDetailsUserControlIsVisible = true;
                }
                else if(viewModel.GoBackFromAddItem == "SoldItemDetailsUserControl")
                {
                    viewModel.SoldItemDetailsUserControlIsVisible = true;
                }
                else if (viewModel.GoBackFromAddItem == "ItemUserControl")
                {
                    viewModel.ItemsUserControlIsVisible = true;
                }
                else if (viewModel.GoBackFromAddItem == "HomeUserControl")
                {
                    viewModel.HomeIsVisible = true;
                }
                else if (viewModel.GoBackFromAddItem == "EditItem")
                {
                    viewModel.ItemDetailsUserControlIsVisible = true;
                }
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
                (BindingContext as HomePageViewModel).CategoryIdForNewItem = 0;
                (BindingContext as HomePageViewModel).CategoryNameForNewItem = string.Empty;
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
            var UserId = Application.Current.Properties["LogedInUserId"];
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
                    if (!viewModel.CategoryList.Contains(viewModel.CategoryList.Where(i => i.CategoryName == "Default").FirstOrDefault()))
                    {
                        viewModel.CategoryList.Add(new CategoryModel { CollectionId = 0, CategoryName = "Default", CreatorId = (int)UserId, ModifierId = (int)UserId });
                    }
                    //viewModel.CategoryList = new ObservableCollection<CategoryModel>(viewModel.CategoryList);
                }
                else
                {
                    viewModel.CategoryList.Add(new CategoryModel { CollectionId = 0, CategoryName = "Default", CreatorId = (int)UserId, ModifierId = (int)UserId });
                }
                viewModel.CategoryList = new ObservableCollection<CategoryModel>(viewModel.CategoryList.OrderBy(x => x.CategoryName).ToList());
            }
            catch (Exception e)
            {

            }
        }
        private void AddMultiplePhotos_Tapped(object sender, EventArgs e)
        {
            try
            {
                var ViewModel = BindingContext as HomePageViewModel;
                if (ViewModel.Base64ItemImagesList == null || ViewModel.Base64ItemImagesList.Count == 0)
                {
                    cross1.IsVisible = false;
                    cross2.IsVisible = false;
                    cross3.IsVisible = false;
                }
                else
                {
                    cross1.IsVisible = true;
                    if (ViewModel.Base64ItemImagesList.Count == 2)
                        cross2.IsVisible = true;
                    else if (ViewModel.Base64ItemImagesList.Count == 3)
                    {
                        cross2.IsVisible = true;
                        cross3.IsVisible = true;
                    }
                }
                //ViewModel.newItemImage = pic;
                //ViewModel.newItemImage_one = pic1;
                //ViewModel.newItemImage_two = pic2;
                //ViewModel.newItemImage_three = pic3;
                var Toast = DependencyService.Get<IMessage>();
                ViewModel.AddMultipleItemPhotosIsVisible = true;
            }
            catch (Exception ex)
            {

            }
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
                    PhotoSize = PhotoSize.Small,
                });
                if (file == null)
                    return;
                (((sender as Grid).Children[1] as Frame).Children[0] as Image).Source = ImageSource.FromStream(() =>
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
                if (string.IsNullOrEmpty(_vm.CollectionNameForNewItem))
                {
                    Toast.LongAlert("Please select collection name."); return;
                }
                if (string.IsNullOrEmpty(_vm.CategoryNameForNewItem))
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
                    cross1.IsVisible = false;
                    cross2.IsVisible = false;
                    cross3.IsVisible = false;
                }
            }
        }

        private async void AddImage1_Tapped(object sender, EventArgs e)
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
                    PhotoSize = PhotoSize.MaxWidthHeight,
                });
                if (file == null)
                    return;
                (((sender as Grid).Children[1] as Frame).Children[0] as Image).Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
                ((sender as Grid).Children[2] as Image).IsVisible = true;
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
                if (ViewModel.Base64ItemImagesList == null || ViewModel.Base64ItemImagesList.Count == 0)
                {
                    ViewModel.Base64ItemImagesList = new List<string>();
                    ViewModel.Base64ItemImagesList.Add(base64Img);
                }
                else
                {
                    ViewModel.Base64ItemImagesList[0] = base64Img;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void AddImage2_Tapped(object sender, EventArgs e)
        {
            var Toast = DependencyService.Get<IMessage>();
            var ViewModel = BindingContext as HomePageViewModel;
            if (ViewModel.Base64ItemImagesList == null || ViewModel.Base64ItemImagesList.Count == 0)
                Toast.LongAlert("Please add first Image.");
            else
            {
                try
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        Toast.LongAlert("No Camera available");
                        return;
                    }
                    var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = PhotoSize.MaxWidthHeight,
                    });
                    if (file == null)
                        return;
                    (((sender as Grid).Children[1] as Frame).Children[0] as Image).Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        return stream;
                    });
                    ((sender as Grid).Children[2] as Image).IsVisible = true;
                    var st = file.GetStream();
                    FileStream fs = st as FileStream;


                    ViewModel.ImageStream = st;
                    byte[] byteArray = ConvertToByteArrayFromStream();
                    string base64Img = Convert.ToBase64String(byteArray);
                    if (ViewModel.Base64ItemImagesList.Count > 1)
                        ViewModel.Base64ItemImagesList[1] = base64Img;
                    else
                        ViewModel.Base64ItemImagesList.Insert(1, base64Img);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private async void AddImage3_Tapped(object sender, EventArgs e)
        {
            var Toast = DependencyService.Get<IMessage>();
            var ViewModel = BindingContext as HomePageViewModel;
            if (ViewModel.Base64ItemImagesList == null || ViewModel.Base64ItemImagesList.Count == 0)
                Toast.LongAlert("Please add first Image.");
            else if (ViewModel.Base64ItemImagesList != null && ViewModel.Base64ItemImagesList.Count == 1)
                Toast.LongAlert("Please add second Image.");
            else
            {
                try
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        Toast.LongAlert("No Camera available");
                        return;
                    }
                    var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = PhotoSize.MaxWidthHeight,
                    });
                    if (file == null)
                        return;
                    (((sender as Grid).Children[1] as Frame).Children[0] as Image).Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        return stream;
                    });
                    ((sender as Grid).Children[2] as Image).IsVisible = true;
                    var st = file.GetStream();
                    FileStream fs = st as FileStream;


                    ViewModel.ImageStream = st;
                    byte[] byteArray = ConvertToByteArrayFromStream();
                    string base64Img = Convert.ToBase64String(byteArray);
                    if (ViewModel.Base64ItemImagesList.Count > 2)
                        ViewModel.Base64ItemImagesList[2] = base64Img;
                    else
                        ViewModel.Base64ItemImagesList.Insert(2, base64Img);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void RemoveImage1_Tapped(object sender, EventArgs e)
        {
            try
            {
                var _vm = BindingContext as HomePageViewModel;
                if (_vm.Base64ItemImagesList.Count == 1)
                {
                    _vm.Base64ItemImagesList.RemoveAt(0);
                    pic1.Source = string.Empty;
                    cross1.IsVisible = false;
                    pic.Source = string.Empty;
                }
                else if (_vm.Base64ItemImagesList.Count == 2)
                {
                    _vm.Base64ItemImagesList[0] = _vm.Base64ItemImagesList[1];
                    _vm.Base64ItemImagesList.RemoveAt(1);
                    pic1.Source = pic2.Source;
                    pic2.Source = string.Empty;
                    cross2.IsVisible = false;
                    pic.Source = pic1.Source;
                }
                else
                {
                    _vm.Base64ItemImagesList[0] = _vm.Base64ItemImagesList[1];
                    _vm.Base64ItemImagesList[1] = _vm.Base64ItemImagesList[2];
                    _vm.Base64ItemImagesList.RemoveAt(2);
                    pic1.Source = pic2.Source;
                    pic2.Source = pic3.Source;
                    pic3.Source = string.Empty;
                    cross3.IsVisible = false;
                    pic.Source = pic1.Source;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void RemoveImage2_Tapped(object sender, EventArgs e)
        {
            try
            {
                var _vm = BindingContext as HomePageViewModel;

                if (_vm.Base64ItemImagesList.Count == 2)
                {
                    _vm.Base64ItemImagesList.RemoveAt(1);
                    pic2.Source = string.Empty;
                    cross2.IsVisible = false;
                }
                else if (_vm.Base64ItemImagesList.Count == 3)
                {
                    _vm.Base64ItemImagesList[1] = _vm.Base64ItemImagesList[2];
                    _vm.Base64ItemImagesList.RemoveAt(2);
                    pic2.Source = pic3.Source;
                    pic3.Source = string.Empty;
                    cross3.IsVisible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void RemoveImage3_Tapped(object sender, EventArgs e)
        {
            try
            {
                var _vm = BindingContext as HomePageViewModel;
                if (_vm.Base64ItemImagesList.Count == 3)
                {
                    _vm.Base64ItemImagesList.RemoveAt(2);
                    pic3.Source = string.Empty;
                    cross3.IsVisible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
