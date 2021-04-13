using HeartlandArtifact.Helpers;
using HeartlandArtifact.Interfaces;
using HeartlandArtifact.Models;
using HeartlandArtifact.Services.Contracts;
using HeartlandArtifact.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;

namespace HeartlandArtifact.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private bool _addNewItemUserControlIsVisible;
        public bool AddNewItemUserControlIsVisible
        {
            get { return _addNewItemUserControlIsVisible; }
            set { SetProperty(ref _addNewItemUserControlIsVisible, value); }
        } 
        private bool _soldItemDetailsUserControlIsVisible;
        public bool SoldItemDetailsUserControlIsVisible
        {
            get { return _soldItemDetailsUserControlIsVisible; }
            set { SetProperty(ref _soldItemDetailsUserControlIsVisible, value); }
        }
        private bool _itemDetailsUserControlIsVisible;
        public bool ItemDetailsUserControlIsVisible
        {
            get { return _itemDetailsUserControlIsVisible; }
            set { SetProperty(ref _itemDetailsUserControlIsVisible, value); }
        }
        private bool _itemsUserControlIsVisible;
        public bool ItemsUserControlIsVisible
        {
            get { return _itemsUserControlIsVisible; }
            set { SetProperty(ref _itemsUserControlIsVisible, value); }
        }
        private bool _contentViewIsVisible;
        public bool ContentViewIsVisible
        {
            get { return _contentViewIsVisible; }
            set { SetProperty(ref _contentViewIsVisible, value); }
        }
        private bool _collectionDropdownIsVisible;
        public bool CollectionDropdownIsVisible
        {
            get { return _collectionDropdownIsVisible; }
            set { SetProperty(ref _collectionDropdownIsVisible, value); }
        }
        private bool _categoryDropdownIsVisible;
        public bool CategoryDropdownIsVisible
        {
            get { return _categoryDropdownIsVisible; }
            set { SetProperty(ref _categoryDropdownIsVisible, value); }
        }
        private bool _soldItemsIsVisible;
        public bool SoldItemsIsVisible
        {
            get { return _soldItemsIsVisible; }
            set { SetProperty(ref _soldItemsIsVisible, value); }
        }
        private string _newCollectionName;
        public string NewCollectionName
        {
            get { return _newCollectionName; }
            set { SetProperty(ref _newCollectionName, value); }
        }
        private bool _homeIsVisible;
        public bool HomeIsVisible
        {
            get { return _homeIsVisible; }
            set { SetProperty(ref _homeIsVisible, value); }
        }
        private bool _categoryUserControlIsVisible;
        public bool CategoryUserControlIsVisible
        {
            get { return _categoryUserControlIsVisible; }
            set { SetProperty(ref _categoryUserControlIsVisible, value); }
        }
        private bool _isEditIconVisible;
        public bool IsEditIconVisible
        {
            get { return _isEditIconVisible; }
            set { SetProperty(ref _isEditIconVisible, value); }
        }
        private bool _myCollectionVisible;
        public bool MyCollectionVisible
        {
            get { return _myCollectionVisible; }
            set { SetProperty(ref _myCollectionVisible, value); }
        }
        private bool _editCollectionPopupIsVisible;
        public bool EditCollectionPopupIsVisible
        {
            get { return _editCollectionPopupIsVisible; }
            set { SetProperty(ref _editCollectionPopupIsVisible, value); }
        }
        private bool _deleteCollectionPopupIsVisible;
        public bool DeleteCollectionPopupIsVisible
        {
            get { return _deleteCollectionPopupIsVisible; }
            set { SetProperty(ref _deleteCollectionPopupIsVisible, value); }
        }
        private bool _addCollectionPopupIsVisible;
        public bool AddCollectionPopupIsVisible
        {
            get { return _addCollectionPopupIsVisible; }
            set { SetProperty(ref _addCollectionPopupIsVisible, value); }
        }
        private bool _addCategoryPopupIsVisible;
        public bool AddCategoryPopupIsVisible
        {
            get { return _addCategoryPopupIsVisible; }
            set { SetProperty(ref _addCategoryPopupIsVisible, value); }
        }
        private bool _notFoundLblIsVisible;
        public bool NotFoundLblIsVisible
        {
            get { return _notFoundLblIsVisible; }
            set { SetProperty(ref _notFoundLblIsVisible, value); }
        }
        private bool _categoryNotFoundLblIsVisible;
        public bool CategoryNotFoundLblIsVisible
        {
            get { return _categoryNotFoundLblIsVisible; }
            set { SetProperty(ref _categoryNotFoundLblIsVisible, value); }
        }
        private bool _itemNotFoundLblIsVisible;
        public bool ItemNotFoundLblIsVisible
        {
            get { return _itemNotFoundLblIsVisible; }
            set { SetProperty(ref _itemNotFoundLblIsVisible, value); }
        }
        private bool _isEditCategoryIconVisible;
        public bool IsEditCategoryIconVisible
        {
            get { return _isEditCategoryIconVisible; }
            set { SetProperty(ref _isEditCategoryIconVisible, value); }
        }
        private bool _deleteItemIconIsVisible;
        public bool DeleteItemIconIsVisible
        {
            get { return _deleteItemIconIsVisible; }
            set { SetProperty(ref _deleteItemIconIsVisible, value); }
        }
        private bool _editCategoryPopupIsVisible;
        public bool EditCategoryPopupIsVisible
        {
            get { return _editCategoryPopupIsVisible; }
            set { SetProperty(ref _editCategoryPopupIsVisible, value); }
        }
        private bool _logoutPopupIsVisible;
        public bool LogoutPopupIsVisible
        {
            get { return _logoutPopupIsVisible; }
            set { SetProperty(ref _logoutPopupIsVisible, value); }
        }
        private bool _deleteCategoryPopupIsVisible;
        public bool DeleteCategoryPopupIsVisible
        {
            get { return _deleteCategoryPopupIsVisible; }
            set { SetProperty(ref _deleteCategoryPopupIsVisible, value); }
        }
        private bool _deleteItemPopupIsVisible;
        public bool DeleteItemPopupIsVisible
        {
            get { return _deleteItemPopupIsVisible; }
            set { SetProperty(ref _deleteItemPopupIsVisible, value); }
        }
        private bool _addMultipleItemPhotosIsVisible;
        public bool AddMultipleItemPhotosIsVisible
        {
            get { return _addMultipleItemPhotosIsVisible; }
            set { SetProperty(ref _addMultipleItemPhotosIsVisible, value); }
        }
        private bool _markAsSoldDetailsIsVisible;
        public bool MarkAsSoldDetailsIsVisible
        {
            get { return _markAsSoldDetailsIsVisible; }
            set { SetProperty(ref _markAsSoldDetailsIsVisible, value); }
        }   
        private bool _dateLabelIsVisible;
        public bool DateLabelIsVisible
        {
            get { return _dateLabelIsVisible; }
            set { SetProperty(ref _dateLabelIsVisible, value); }
        }
        private string _newCategoryName;
        public string NewCategoryName
        {
            get { return _newCategoryName; }
            set { SetProperty(ref _newCategoryName, value); }
        }
        private string _selectedCollectionName;
        public string SelectedCollectionName
        {
            get { return _selectedCollectionName; }
            set { SetProperty(ref _selectedCollectionName, value); }
        }
        private string _selectedCategoryName;
        public string SelectedCategoryName
        {
            get { return _selectedCategoryName; }
            set { SetProperty(ref _selectedCategoryName, value); }
        }

        //Add Item properties

        private int _collectionIdForNewItem;
        public int CollectionIdForNewItem
        {
            get { return _collectionIdForNewItem; }
            set { SetProperty(ref _collectionIdForNewItem, value); }
        }
        private int _categoryIdForNewItem;
        public int CategoryIdForNewItem
        {
            get { return _categoryIdForNewItem; }
            set { SetProperty(ref _categoryIdForNewItem, value); }
        }
        private string _collectionNameForNewItem;
        public string CollectionNameForNewItem
        {
            get { return _collectionNameForNewItem; }
            set { SetProperty(ref _collectionNameForNewItem, value); }
        }
        private string _categoryNameForNewItem;
        public string CategoryNameForNewItem
        {
            get { return _categoryNameForNewItem; }
            set { SetProperty(ref _categoryNameForNewItem, value); }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private string _material;
        public string Material
        {
            get { return _material; }
            set { SetProperty(ref _material, value); }
        }
        private string _foundBy;
        public string FoundBy
        {
            get { return _foundBy; }
            set { SetProperty(ref _foundBy, value); }
        }
        private string _exCollection;
        public string ExCollection
        {
            get { return _exCollection; }
            set { SetProperty(ref _exCollection, value); }
        }
        private string _perceivedValue;
        public string PerceivedValue
        {
            get { return _perceivedValue; }
            set { SetProperty(ref _perceivedValue, value); }
        }
        private string _cost;
        public string Cost
        {
            get { return _cost; }
            set { SetProperty(ref _cost, value); }
        }
        private string _length;
        public string Length
        {
            get { return _length; }
            set { SetProperty(ref _length, value); }
        }
        private string _country;
        public string Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }
        private string _state;
        public string State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }
        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set { SetProperty(ref _notes, value); }
        }
        private string _itemImageSource;
        public string ItemImageSource
        {
            get { return _itemImageSource; }
            set { SetProperty(ref _itemImageSource, value); }
        }
       

        // Item Properties
       
        private string _itemTitle;
        public string ItemTitle
        {
            get { return _itemTitle; }
            set { SetProperty(ref _itemTitle, value); }
        }
        private string _itemMaterial;
        public string ItemMaterial
        {
            get { return _itemMaterial; }
            set { SetProperty(ref _itemMaterial, value); }
        }
        private string _itemFoundBy;
        public string ItemFoundBy
        {
            get { return _itemFoundBy; }
            set { SetProperty(ref _itemFoundBy, value); }
        }
        private string _itemExCollection;
        public string ItemExCollection
        {
            get { return _itemExCollection; }
            set { SetProperty(ref _itemExCollection, value); }
        }
        private string _itemPerceivedValue;
        public string ItemPerceivedValue
        {
            get { return _itemPerceivedValue; }
            set { SetProperty(ref _itemPerceivedValue, value); }
        }
        private string _itemCost;
        public string ItemCost
        {
            get { return _itemCost; }
            set { SetProperty(ref _itemCost, value); }
        }
        private string _itemLength;
        public string ItemLength
        {
            get { return _itemLength; }
            set { SetProperty(ref _itemLength, value); }
        }
        private string _itemCountry;
        public string ItemCountry
        {
            get { return _itemCountry; }
            set { SetProperty(ref _itemCountry, value); }
        }
        private string _itemState;
        public string ItemState
        {
            get { return _itemState; }
            set { SetProperty(ref _itemState, value); }
        }
        private string _itemNotes;
        public string ItemNotes
        {
            get { return _itemNotes; }
            set { SetProperty(ref _itemNotes, value); }
        }
        private string _soldPrice;
        public string SoldPrice
        {
            get { return _soldPrice; }
            set { SetProperty(ref _soldPrice, value); }
        }
        private string _soldDate;
        public string SoldDate
        {
            get { return _soldDate; }
            set { SetProperty(ref _soldDate, value); }
        }
        private ObservableCollection<CategoryModel> _allUserCategories;
        public ObservableCollection<CategoryModel> AllUserCategories
        {
            get { return _allUserCategories; }
            set { SetProperty(ref _allUserCategories, value); }
        }
        private ObservableCollection<CollectionModel> _allCollections;
        public ObservableCollection<CollectionModel> AllCollections
        {
            get { return _allCollections; }
            set { SetProperty(ref _allCollections, value); }
        }
        private ObservableCollection<CategoryModel> _allCategories;
        public ObservableCollection<CategoryModel> AllCategories
        {
            get { return _allCategories; }
            set { SetProperty(ref _allCategories, value); }
        }
        private ObservableCollection<MyItemModel> _allItems;
        public ObservableCollection<MyItemModel> AllItems
        {
            get { return _allItems; }
            set { SetProperty(ref _allItems, value); }
        }
        private ObservableCollection<CollectionModel> _collectionList;
        public ObservableCollection<CollectionModel> CollectionList
        {
            get { return _collectionList; }
            set { SetProperty(ref _collectionList, value); }
        }
        private ObservableCollection<CategoryModel> _categoryList;
        public ObservableCollection<CategoryModel> CategoryList
        {
            get { return _categoryList; }
            set { SetProperty(ref _categoryList, value); }
        }
        private ObservableCollection<MyItemModel> _itemImagesForCarousel;
        public ObservableCollection<MyItemModel> ItemImagesForCarousel
        {
            get { return _itemImagesForCarousel; }
            set { SetProperty(ref _itemImagesForCarousel, value); }
        }
        public DelegateCommand LogoutCommand { get; set; }
        public DelegateCommand EditCollectionCommand { get; set; }
        public DelegateCommand GoBackFromCollectionsCommand { get; set; }
        public DelegateCommand AddCollectionButtonCommand { get; set; }
        public DelegateCommand ConfirmButtonCommand { get; set; }
        public DelegateCommand CrossButtonCommand { get; set; }
        public DelegateCommand<CollectionModel> OpenDeleteCollectionPopupCommand { get; set; }
        public DelegateCommand<CollectionModel> OpenUpdateCollectionPopupCommand { get; set; }
        public DelegateCommand SaveButtonCommand { get; set; }
        public DelegateCommand CancelButtonCommand { get; set; }
        public DelegateCommand CancelDeleteButtonCommand { get; set; }
        public DelegateCommand DeleteCollectionCommand { get; set; }
        public DelegateCommand CloseAddCategoryPopupCommand { get; set; }
        public DelegateCommand AddNewCategoryBtnCommand { get; set; }
        public DelegateCommand SaveEditCategoryCommand { get; set; }
        public DelegateCommand CancelEditCategoryCommand { get; set; }
        public DelegateCommand DeleteCategoryCommand { get; set; }
        public DelegateCommand CancelDeleteCategoryCommand { get; set; }
        public DelegateCommand ConfirmLogoutCommand { get; set; }
        public DelegateCommand DeleteItemCommand { get; set; }
        public DelegateCommand CancelDeleteItemCommand { get; set; }
        public DelegateCommand AddNewItemCommand { get; set; }
        public INavigationService _nav;
        public CollectionModel CollectionData { get; set; }
        public CategoryModel CategoryData { get; set; }
        public MyItemModel ItemData { get; set; }
        public int ItemId { get; set; }
        private readonly IGoogleManager _googleManager;
        private readonly IFacebookManager _facebookManager;
        public Stream ImageStream { get; set; }
        public List<string> Base64ItemImagesList { get; set; }
        public string GoBackFromAddItem { get; set; }
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _nav = navigationService;
            HomeIsVisible = true;
            Base64ItemImagesList = new List<string>();
            _facebookManager = DependencyService.Get<IFacebookManager>();
            _googleManager = DependencyService.Get<IGoogleManager>();
            UserName = "Hi, " + Application.Current.Properties["UserName"].ToString();
            GetUserCollections();
            GetAllUserCategories();
            // LogoutCommand = new DelegateCommand(Logout);
            EditCollectionCommand = new DelegateCommand(EditCollection);
            GoBackFromCollectionsCommand = new DelegateCommand(GoBackFromCollection);
            AddCollectionButtonCommand = new DelegateCommand(OpenCloseAddCollectionPopup);
            CrossButtonCommand = new DelegateCommand(OpenCloseAddCollectionPopup);
            ConfirmButtonCommand = new DelegateCommand(CreateNewCollection);
            OpenUpdateCollectionPopupCommand = new DelegateCommand<CollectionModel>(OpenUpdateCollectionPopup);
            OpenDeleteCollectionPopupCommand = new DelegateCommand<CollectionModel>(OpenCloseDeleteCollectionPopup);
            CancelButtonCommand = new DelegateCommand(CloseUpdateCollectionPopup);
            SaveButtonCommand = new DelegateCommand(UpdateCollectionName);
            DeleteCollectionCommand = new DelegateCommand(DeleteCollection);
            CancelDeleteButtonCommand = new DelegateCommand(CloseDeleteCollectionPopup);
            CloseAddCategoryPopupCommand = new DelegateCommand(CloseAddCategoryPopup);
            AddNewCategoryBtnCommand = new DelegateCommand(CreateNewCategory);
            SaveEditCategoryCommand = new DelegateCommand(UpdateCategoryName);
            CancelEditCategoryCommand = new DelegateCommand(CloseUpdateCategoryPopup);
            DeleteCategoryCommand = new DelegateCommand(DeleteCategory);
            CancelDeleteCategoryCommand = new DelegateCommand(CloseDeleteCategoryPopup);
            ConfirmLogoutCommand = new DelegateCommand(Logout);
            AddNewItemCommand = new DelegateCommand(AddNewItem);
            DeleteItemCommand = new DelegateCommand(DeleteItem);
            CancelDeleteItemCommand = new DelegateCommand(CloseDeleteItemPopup);
        }
        public async void Logout()
        {
            IsBusy = true;
            LogoutPopupIsVisible = false;
            _facebookManager.Logout();
            _googleManager.Logout();
            Application.Current.Properties["IsLogedIn"] = false;
            Application.Current.Properties["LogedInUserId"] = 0;
            Application.Current.Properties["UserName"] = string.Empty;
            await Application.Current.SavePropertiesAsync();
            await _nav.NavigateAsync("SignInPage");
            IsBusy = false;
        }
        public async void GetUserCollections()
        {
            try
            {
                IsBusy = true;
                var UserId = Application.Current.Properties["LogedInUserId"];
                var response = await new ApiData().GetData<List<CollectionModel>>("Collections/GetUserCollections?userId=" + UserId, true);
                if (response != null)
                {
                    AllCollections = new ObservableCollection<CollectionModel>(response.data);
                }
                NotFoundLblIsVisible = AllCollections.Count > 0 ? false : true;
                IsBusy = false;
            }
            catch (Exception e)
            {

            }
        }
        public async void GetAllUserCategories()
        {
            try
            {
                IsBusy = true;
                AllUserCategories = new ObservableCollection<CategoryModel>();
                var UserId = Application.Current.Properties["LogedInUserId"];
                var response = await new ApiData().GetData<List<CategoryModel>>("Category/GetUserCategories?userId=" + UserId, true);
                if (response != null)
                {
                    AllUserCategories = new ObservableCollection<CategoryModel>(response.data);
                }
                IsBusy = false;
            }
            catch (Exception e)
            {

            }
        }
        public async void GetUserCategories(CollectionModel collection)
        {
            try
            {
                IsBusy = true;
                AllCategories = new ObservableCollection<CategoryModel>();
                CollectionData = new CollectionModel();
                CollectionData = collection;
                SelectedCollectionName = collection.CollectionName;
                var UserId = Application.Current.Properties["LogedInUserId"];
                var response = await new ApiData().GetData<List<CategoryModel>>("Category/GetUserCategories?userId=" + UserId, true);
                if (response != null)
                {
                    foreach (var item in response.data)
                    {
                        if (item.CollectionId == CollectionData.CollectionId)
                            AllCategories.Add(item);
                    }
                    //AllCategories = new ObservableCollection<CategoryModel>(response.data.Where(i => i.CollectionId == CollectionData.CollectionId));
                }
                CategoryNotFoundLblIsVisible = AllCategories.Count > 0 ? false : true;
                IsBusy = false;
            }
            catch (Exception e)
            {

            }
        }
        public async void GetUserItems(CategoryModel category)
        {
            try
            {
                IsBusy = true;
                CategoryData = new CategoryModel();
                CategoryData = category;
                SelectedCategoryName = category.CategoryName;
                var UserId = Application.Current.Properties["LogedInUserId"];
                AllItems = new ObservableCollection<MyItemModel>();
                var response = await new ApiData().GetData<List<ApiItemModel>>("Artifact/GetUserItems?userId=" + UserId, true);
                if (response != null && response.data != null)
                {
                    foreach (var item in response.data)
                    {
                        if (item.item.CollectionId == CategoryData.CollectionId && item.item.CategoryId == CategoryData.CategoryId)
                        {
                            AllItems.Add(new MyItemModel
                            {
                                ItemId = item.item.ItemId,
                                CollectionId = item.item.CollectionId,
                                CategoryId = item.item.CategoryId,
                                Title = item.item.Title,
                                Material = item.item.Material,
                                IsItemSold = item.item.IsItemSold,
                                FoundBy = item.item.FoundBy,
                                ExCollection = item.item.ExCollection,
                                PerceivedValue = item.item.PerceivedValue,
                                Cost = item.item.Cost,
                                Length = item.item.Length,
                                Country = item.item.Country,
                                State = item.item.State,
                                Notes = item.item.Notes,
                                CreateDate = item.item.CreateDate,
                                LastModDate = item.item.LastModDate,
                                CreatorId = item.item.CreatorId,
                                ModifierId = item.item.ModifierId,
                                ImageUrl = item.images.Count > 0 ? item.images[0] : ""
                            });
                        }
                    }
                }
                ItemNotFoundLblIsVisible = AllItems.Count > 0 ? false : true;
                IsBusy = false;
            }
            catch (Exception e)
            {

            }
        }
        public async void CreateNewCollection()
        {
            var Toast = DependencyService.Get<IMessage>();
            //if (string.IsNullOrEmpty(NewCollectionName))
            //{
            //    Toast.LongAlert("Please enter a collection name.");
            //    return;
            //}
            if (NewCollectionName.Length == 0 || NewCollectionName.Length > 30)
            {
                Toast.LongAlert("Collection name must be between 1 to 30 characters long.");
                return;
            }
            else
            {
                try
                {
                    IsBusy = true;
                    var newCollection = new CollectionModel()
                    {
                        CollectionName = NewCollectionName,
                        CreatorId = (int)Application.Current.Properties["LogedInUserId"],
                        ModifierId = (int)Application.Current.Properties["LogedInUserId"],
                    };
                    var response = await new ApiData().PostData<CollectionModel>("Collections/AddNewCollection", newCollection, true);
                    if (response != null && response.data != null)
                    {
                        //AllCollections.Add(response.data);
                        AllCollections.Insert(0, response.data);
                    }
                    else
                    {
                        Toast.LongAlert(response.message);
                        IsBusy = false;
                        return;
                    }
                    NotFoundLblIsVisible = AllCollections.Count > 0 ? false : true;
                    if (IsEditIconVisible)
                    {
                        IsEditIconVisible = false;
                    }
                    AddCollectionPopupIsVisible = false;
                    NewCollectionName = string.Empty;
                    IsBusy = false;
                }
                catch (Exception e)
                {

                }
            }
        }
        public async void CreateNewCategory()
        {
            var Toast = DependencyService.Get<IMessage>();
            //if (string.IsNullOrEmpty(NewCategoryName))
            //{
            //    Toast.LongAlert("Please enter a category name.");
            //    return;
            //}
            if (NewCategoryName.Length == 0 || NewCategoryName.Length > 30)
            {
                Toast.LongAlert("Category name must be between 1 to 30 characters long.");
                return;
            }
            else
            {
                try
                {
                    IsBusy = true;
                    var newCategory = new CategoryModel()
                    {
                        CollectionId = CollectionData.CollectionId,
                        CategoryName = NewCategoryName,
                        CreatorId = (int)Application.Current.Properties["LogedInUserId"],
                        ModifierId = (int)Application.Current.Properties["LogedInUserId"],
                    };
                    var response = await new ApiData().PostData<CategoryModel>("Category/AddNewCategory", newCategory, true);
                    if (response != null && response.data != null)
                    {
                        AllCategories.Insert(0, response.data);
                    }
                    else
                    {
                        Toast.LongAlert(response.message);
                        IsBusy = false;
                        return;
                    }
                    CategoryNotFoundLblIsVisible = AllCategories.Count > 0 ? false : true;
                    if (IsEditCategoryIconVisible)
                    {
                        IsEditCategoryIconVisible = false;
                    }
                    AddCategoryPopupIsVisible = false;
                    NewCategoryName = string.Empty;
                    IsBusy = false;
                }
                catch (Exception e)
                {

                }
            }
        }
        public async void UpdateCollectionName()
        {
            var Toast = DependencyService.Get<IMessage>();
            //if (string.IsNullOrEmpty(NewCollectionName))
            //{
            //    Toast.LongAlert("Please enter a collection name.");
            //    return;
            //}
            if (NewCollectionName.Length == 0 || NewCollectionName.Length > 30)
            {
                Toast.LongAlert("Collection name must be between 1 to 30 characters long.");
                return;
            }
            else
            {
                try
                {
                    IsEditIconVisible = false;
                    IsBusy = true;
                    var collection = new CollectionModel()
                    {
                        CollectionId = CollectionData.CollectionId,
                        CollectionName = NewCollectionName,
                        CreatorId = CollectionData.CreatorId,
                        ModifierId = (int)Application.Current.Properties["LogedInUserId"]
                    };
                    var response = await new ApiData().PutData<CollectionModel>("Collections/UpdateCollection", collection, true);
                    if (response != null && response.status == "success")
                    {
                        AllCollections.Remove(CollectionData);
                        AllCollections.Insert(0, response.data);
                        AllCollections = new ObservableCollection<CollectionModel>(AllCollections);
                        // GetUserCollections();
                    }
                    else
                    {
                        Toast.LongAlert(response.message);
                        IsBusy = false;
                        return;
                    }
                    EditCollectionPopupIsVisible = false;
                    NewCollectionName = string.Empty;
                    IsBusy = false;
                }
                catch (Exception e)
                {

                }
            }
        }
        public async void UpdateCategoryName()
        {
            var Toast = DependencyService.Get<IMessage>();
            //if (string.IsNullOrEmpty(NewCategoryName))
            //{
            //    Toast.LongAlert("Please enter a category name.");
            //    return;
            //}
            if (NewCategoryName.Length == 0 || NewCategoryName.Length > 30)
            {
                Toast.LongAlert("Category name must be between 1 to 30 characters long.");
                return;
            }
            else
            {
                try
                {
                    IsEditCategoryIconVisible = false;
                    IsBusy = true;
                    var category = new CategoryModel()
                    {
                        CategoryId = CategoryData.CategoryId,
                        CollectionId = CategoryData.CollectionId,
                        CategoryName = NewCategoryName,
                        CreatorId = CategoryData.CreatorId,
                        ModifierId = (int)Application.Current.Properties["LogedInUserId"]
                    };
                    var response = await new ApiData().PutData<CategoryModel>("Category/UpdateCategory", category, true);
                    if (response != null && response.data != null)
                    {
                        AllCategories.Remove(CategoryData);
                        AllCategories.Insert(0, response.data);
                        AllCategories = new ObservableCollection<CategoryModel>(AllCategories);
                    }
                    else
                    {
                        Toast.LongAlert(response.message);
                        IsBusy = false;
                        return;
                    }
                    EditCategoryPopupIsVisible = !EditCategoryPopupIsVisible;
                    NewCategoryName = string.Empty;
                    IsBusy = false;
                }
                catch (Exception e)
                { }
            }
        }
        public async void DeleteCollection()
        {
            IsEditIconVisible = false;
            IsBusy = true;
            var response = await new ApiData().DeleteData<string>("Collections/DeleteCollectionById?collectionId=" + CollectionData.CollectionId, true);
            if (response != null)
            {
                AllCollections.Remove(CollectionData);
                GetUserCollections();
            }
            NotFoundLblIsVisible = AllCollections.Count > 0 ? false : true;
            DeleteCollectionPopupIsVisible = false;
            IsBusy = false;
        }
        public async void DeleteCategory()
        {
            IsEditCategoryIconVisible = false;
            IsBusy = true;
            var response = await new ApiData().DeleteData<string>("Category/DeleteCategoryById?categoryId=" + CategoryData.CategoryId, true);
            if (response != null)
            {
                AllCategories.Remove(CategoryData);
                AllCategories = new ObservableCollection<CategoryModel>(AllCategories);
                //RefreshCategoryList();
            }
            CategoryNotFoundLblIsVisible = AllCategories.Count > 0 ? false : true;
            DeleteCategoryPopupIsVisible = !DeleteCategoryPopupIsVisible;
            IsBusy = false;
        }
        public void EditCollection()
        {
            IsEditIconVisible = true;
        }
        public void OpenUpdateCollectionPopup(CollectionModel Collection)
        {
            CollectionData = new CollectionModel();
            CollectionData = Collection;
            NewCollectionName = Collection.CollectionName;
            EditCollectionPopupIsVisible = !EditCollectionPopupIsVisible;
        }
        public void CloseUpdateCollectionPopup()
        {
            IsEditIconVisible = false;
            NewCollectionName = string.Empty;
            EditCollectionPopupIsVisible = !EditCollectionPopupIsVisible;
        }
        public void OpenCloseDeleteCollectionPopup(CollectionModel Collection)
        {
            IsEditIconVisible = false;
            CollectionData = new CollectionModel();
            CollectionData = Collection;
            DeleteCollectionPopupIsVisible = !DeleteCollectionPopupIsVisible;
        }
        public void CloseDeleteCollectionPopup()
        {
            IsEditIconVisible = false;
            DeleteCollectionPopupIsVisible = !DeleteCollectionPopupIsVisible;
        }
        public void GoBackFromCollection()
        {
            if (IsEditIconVisible)
            {
                IsEditIconVisible = false;
            }
            else
            {
                HomeIsVisible = true;
                MyCollectionVisible = false;
            }
        }
        public void OpenCloseAddCollectionPopup()
        {
            NewCollectionName = string.Empty;
            AddCollectionPopupIsVisible = !AddCollectionPopupIsVisible;
        }
        public void CloseAddCategoryPopup()
        {
            NewCategoryName = string.Empty;
            AddCategoryPopupIsVisible = !AddCategoryPopupIsVisible;
        }
        public void CloseUpdateCategoryPopup()
        {
            IsEditCategoryIconVisible = false;
            NewCategoryName = string.Empty;
            EditCategoryPopupIsVisible = !EditCategoryPopupIsVisible;
        }
        public void CloseDeleteCategoryPopup()
        {
            IsEditCategoryIconVisible = false;
            DeleteCategoryPopupIsVisible = !DeleteCategoryPopupIsVisible;
        }
        public async void AddNewItem()
        {
            var Toast = DependencyService.Get<IMessage>();
            try
            {
                IsBusy = true;
                var newItem = new ItemModel();
                newItem.ItemId = 0;
                newItem.CollectionId = CollectionIdForNewItem;
                newItem.CategoryId = CategoryIdForNewItem;
                newItem.Title = Title;
                newItem.Material = Material;
                newItem.FoundBy = FoundBy;
                newItem.ExCollection = ExCollection;
                newItem.PerceivedValue = PerceivedValue;
                newItem.Cost = Cost;
                newItem.Length = Length;
                newItem.Country = Country;
                newItem.State = State;
                newItem.Notes = Notes;
                newItem.CreatorId = (int)Application.Current.Properties["LogedInUserId"];
                newItem.ModifierId = (int)Application.Current.Properties["LogedInUserId"];
                newItem.base64ItemImages = new List<string>();
                newItem.base64ItemImages = Base64ItemImagesList;
                var response = await new ApiData().PostData<ApiItemModel>("Artifact/AddNewItem", newItem, true);
                if (response != null && response.data != null)
                {
                    Toast.LongAlert("Item added successfully.");
                    GetItemDetailsById(response.data.item.ItemId);
                    ItemDetailsUserControlIsVisible = true;
                    AddNewItemUserControlIsVisible = false;
                    CollectionNameForNewItem = string.Empty;
                    CategoryNameForNewItem = string.Empty;
                    Title = string.Empty;
                    Material = string.Empty;
                    PerceivedValue = string.Empty;
                    Cost = string.Empty;
                    FoundBy = string.Empty;
                    ExCollection = string.Empty;
                    Length = string.Empty;
                    Country = string.Empty;
                    State = string.Empty;
                    Notes = string.Empty;
                    ItemImageSource = string.Empty;
                }
                else
                {
                    Toast.LongAlert(response.message);
                }
                IsBusy = false;
            }
            catch (Exception e)
            {

            }
        }
        public async void GetItemDetailsById(int ItemID)
        {
            try
            {
                if (ItemID != 0)
                {
                    IsBusy = true;
                    var Item = await new ApiData().GetData<ApiItemModel>("Artifact/GeItemDetailByItemId?itemId=" + ItemID, true);
                    if (Item != null)
                    {
                        var ItemDetails = Item.data;                      
                        ItemId = ItemDetails.item.ItemId;
                        ItemTitle = ItemDetails.item.Title ?? "";
                        ItemMaterial = ItemDetails.item.Material ?? "";
                        ItemFoundBy = ItemDetails.item.FoundBy ?? "";
                        ItemExCollection = ItemDetails.item.ExCollection ?? "";
                        ItemPerceivedValue = ItemDetails.item.PerceivedValue ?? "";
                        ItemCost = ItemDetails.item.Cost ?? "";
                        ItemLength = ItemDetails.item.Length ?? "";
                        ItemCountry = ItemDetails.item.Country ?? "";
                        ItemState = ItemDetails.item.State ?? "";
                        ItemNotes = ItemDetails.item.Notes ?? "";
                        ItemImagesForCarousel = new ObservableCollection<MyItemModel>();
                        if (ItemDetails.images.Count > 0)
                            foreach (var i in ItemDetails.images)
                            {
                                ItemImagesForCarousel.Add(new MyItemModel { ImageUrl = i });
                            }
                    }
                    IsBusy = false;
                }
            }
            catch (Exception e)
            {

            }
        }
        public async void DeleteItem()
        {
            DeleteItemIconIsVisible = false;
            IsBusy = true;
            var response = await new ApiData().DeleteData<string>("Artifact/DeleteItemById?itemId=" + ItemData.ItemId, true);
            if (response != null)
            {
                AllItems.Remove(ItemData);
                AllItems = new ObservableCollection<MyItemModel>(AllItems);
            }
            ItemNotFoundLblIsVisible = AllItems.Count > 0 ? false : true;
            DeleteItemPopupIsVisible = !DeleteItemPopupIsVisible;
            IsBusy = false;
        }
        public void CloseDeleteItemPopup()
        {
            DeleteItemIconIsVisible = false;
            DeleteItemPopupIsVisible = !DeleteItemPopupIsVisible;
        }
        public async void ItemMarkAsSold()
        {
            var Toast = DependencyService.Get<IMessage>();
            if (string.IsNullOrEmpty(SoldPrice))
            {
                Toast.LongAlert("Please enter sold price."); return;
            }
            if (string.IsNullOrEmpty(SoldDate))
            {
                Toast.LongAlert("Please enter sold date."); return;
            }
            else
            {
                if (ItemId > 0)
                {
                    IsBusy = true;
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    form.Add(new StringContent("0"), "Id");
                    form.Add(new StringContent(ItemId.ToString()), "ItemId");
                    form.Add(new StringContent(PerceivedValue ?? ""), "PerceivedValue");
                    form.Add(new StringContent(Cost ?? ""), "Cost");
                    form.Add(new StringContent(SoldPrice), "SoldPrice");
                    form.Add(new StringContent(SoldDate), "SoldDate");
                    var response = await new ApiData().PostFormData<ItemMarkAsSoldModel>("Artifact/MarkItemAsSold", form, true);
                    if (response != null && response.data != null)
                    {
                        Toast.LongAlert(response.message);

                       // SoldItemDetailsUserControlIsVisible = true;
                    }
                    else
                    {
                        Toast.LongAlert(response.message);
                    }
                    IsBusy = false;
                }
            }
        }
    }
}



