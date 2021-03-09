using HeartlandArtifact.Helpers;
using HeartlandArtifact.Interfaces;
using HeartlandArtifact.Models;
using HeartlandArtifact.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace HeartlandArtifact.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
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
        private bool _notFoundLblIsVisible;
        public bool NotFoundLblIsVisible
        {
            get { return _notFoundLblIsVisible; }
            set { SetProperty(ref _notFoundLblIsVisible, value); }
        }
        private ObservableCollection<CollectionModel> _allCollections;
        public ObservableCollection<CollectionModel> AllCollections
        {
            get { return _allCollections; }
            set { SetProperty(ref _allCollections, value); }
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
        public INavigationService _nav;

        public CollectionModel CollectionData { get; set; }
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _nav = navigationService;
            HomeIsVisible = true;
            GetAllCollections();
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
        }
        public void Logout()
        {
            Application.Current.Properties["IsLogedIn"] = false;
            Application.Current.MainPage = new SignInPage();
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
            EditCollectionPopupIsVisible = !EditCollectionPopupIsVisible;
        }
        public async void UpdateCollectionName()
        {
            var Toast = DependencyService.Get<IMessage>();
            if (string.IsNullOrEmpty(NewCollectionName))
            {
                Toast.LongAlert("Collection name is required.");
                return;
            }
            else
            {
                IsBusy = true;
                var collection = new CollectionModel()
                {
                    CollectionId = CollectionData.CollectionId,
                    CollectionName = NewCollectionName,
                    CreatorId = CollectionData.CreatorId,
                    ModifierId = App.User.UserId
                };
                var response = await new ApiData().PutData<CollectionModel>("Collections/UpdateCollection", collection, true);
                if (response != null)
                {
                    // AllCollections.Where(c => c.CollectionId == response.data.CollectionId).FirstOrDefault().CollectionName = response.data.CollectionName;
                    AllCollections.Remove(CollectionData);
                    AllCollections.Add(response.data);
                }
                EditCollectionPopupIsVisible = false;
                NewCollectionName = string.Empty;
                IsBusy = false;
            }
        }
        public void OpenCloseDeleteCollectionPopup(CollectionModel Collection)
        {
            CollectionData = new CollectionModel();
            CollectionData = Collection;
            DeleteCollectionPopupIsVisible = !DeleteCollectionPopupIsVisible;
        }
        public void CloseDeleteCollectionPopup()
        {
            DeleteCollectionPopupIsVisible = !DeleteCollectionPopupIsVisible;
        }
        public async void DeleteCollection()
        {
            IsBusy = true;
            var response = await new ApiData().DeleteData<string>("Collections/DeleteCollectionById?collectionId=" + CollectionData.CollectionId, true);
            if (response != null)
            {
                AllCollections.Remove(CollectionData);
            }
            NotFoundLblIsVisible = AllCollections.Count > 0 ? false : true;
            DeleteCollectionPopupIsVisible = false;
            IsBusy = false;
        }
        public async void GetAllCollections()
        {
            IsBusy = true;
            var response = await new ApiData().GetData<List<CollectionModel>>("Collections/GetAllCollections", true);
            if (response != null)
            {
                AllCollections = new ObservableCollection<CollectionModel>(response.data);                
            }
            NotFoundLblIsVisible = AllCollections.Count > 0 ? false : true;
            IsBusy = false;
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
            AddCollectionPopupIsVisible = !AddCollectionPopupIsVisible;
        }
        public async void CreateNewCollection()
        {
            var Toast = DependencyService.Get<IMessage>();
            if (string.IsNullOrEmpty(NewCollectionName))
            {
                Toast.LongAlert("Collection name is required.");
                return;
            }
            else
            {
                IsBusy = true;
                var newCollection = new CollectionModel()
                {
                    CollectionName = NewCollectionName,
                    CreatorId = App.User.UserId,
                };
                var response = await new ApiData().PostData<CollectionModel>("Collections/AddNewCollection", newCollection, true);
                if (response != null)
                {
                    AllCollections.Add(response.data);
                }
                NotFoundLblIsVisible = AllCollections.Count > 0 ? false : true;
                AddCollectionPopupIsVisible = false;
                NewCollectionName = string.Empty;
                IsBusy = false;
            }
        }

    }
}

