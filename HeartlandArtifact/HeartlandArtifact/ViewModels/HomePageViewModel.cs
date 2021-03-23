using HeartlandArtifact.Helpers;
using HeartlandArtifact.Interfaces;
using HeartlandArtifact.Models;
using HeartlandArtifact.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private bool _isEditCategoryIconVisible;
        public bool IsEditCategoryIconVisible
        {
            get { return _isEditCategoryIconVisible; }
            set { SetProperty(ref _isEditCategoryIconVisible, value); }
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
        private ObservableCollection<CollectionModel> _collectionList;
        public ObservableCollection<CollectionModel> CollectionList
        {
            get { return _collectionList; }
            set { SetProperty(ref _collectionList, value); }
        }
        private ObservableCollection<CollectionModel> _categoryList;
        public ObservableCollection<CollectionModel> CategoryList
        {
            get { return _categoryList; }
            set { SetProperty(ref _categoryList, value); }
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
        public INavigationService _nav;
        public CollectionModel CollectionData { get; set; }
        public CategoryModel CategoryData { get; set; }
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _nav = navigationService;
            HomeIsVisible = true;
            UserName = "Hi, " + Application.Current.Properties["UserName"].ToString();
            GetUserCollections();
            LogoutCommand = new DelegateCommand(Logout);
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
            GetListForDropdown();
        }
        public void Logout()
        {
            Application.Current.Properties["IsLogedIn"] = false;
            Application.Current.MainPage = new SignInPage();
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
        public async void CreateNewCollection()
        {
            var Toast = DependencyService.Get<IMessage>();
            if (string.IsNullOrEmpty(NewCollectionName))
            {
                Toast.LongAlert("Please enter a collection name.");
                return;
            }
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
            if (string.IsNullOrEmpty(NewCategoryName))
            {
                Toast.LongAlert("Please enter a category name.");
                return;
            }
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
            if (string.IsNullOrEmpty(NewCollectionName))
            {
                Toast.LongAlert("Please enter a collection name.");
                return;
            }
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
                    if (response != null)
                    {
                        GetUserCollections();
                        // AllCollections.Remove(CollectionData);
                        // AllCollections.Add(response.data);
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
            if (string.IsNullOrEmpty(NewCategoryName))
            {
                Toast.LongAlert("Please enter a category name.");
                return;
            }
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
                        AllCategories.Add(response.data);
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
        public void GetListForDropdown()
        {
            CollectionList = new ObservableCollection<CollectionModel>()
            {
                new CollectionModel{CollectionName="Coins"},
                new CollectionModel{CollectionName="Stones"},
                new CollectionModel{CollectionName="Black stone"},
                new CollectionModel{CollectionName="Black stone"},
                new CollectionModel{CollectionName="Black stone"},
            };
            CategoryList = new ObservableCollection<CollectionModel>()
            {
                new CollectionModel{CollectionName="Coins"},
                new CollectionModel{CollectionName="Stones"},
                new CollectionModel{CollectionName="Black stone"},
                new CollectionModel{CollectionName="Black stone"},
                new CollectionModel{CollectionName="Black stone"},
            };
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
        public async void RefreshCategoryList()
        {
            try
            {
                IsBusy = true;
                var UserId = Application.Current.Properties["LogedInUserId"];
                var response = await new ApiData().GetData<List<CategoryModel>>("Category/GetUserCategories?userId=" + UserId, true);
                if (response != null)
                {
                    AllCategories = new ObservableCollection<CategoryModel>();
                    foreach (var item in response.data)
                    {
                        if (item.CollectionId == CollectionData.CollectionId)
                            AllCategories.Add(item);

                    }
                }
                CategoryNotFoundLblIsVisible = AllCategories.Count > 0 ? false : true;
                IsBusy = false;
            }
            catch (Exception e)
            {

            }
        }
    }
}



