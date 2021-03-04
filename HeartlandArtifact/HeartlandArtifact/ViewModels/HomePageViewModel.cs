using HeartlandArtifact.Helpers;
using HeartlandArtifact.Models;
using HeartlandArtifact.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace HeartlandArtifact.ViewModels
{
    public class HomePageViewModel:ViewModelBase
    {
        private bool _soldItemsIsVisible;
        public bool SoldItemsIsVisible
        {
            get { return _soldItemsIsVisible; }
            set { SetProperty(ref _soldItemsIsVisible, value); }
        }
        private bool _homeIsVisible;
        public bool HomeIsVisible
        {
            get { return _homeIsVisible; }
            set { SetProperty(ref _homeIsVisible, value); }
        }
        private bool _myCollectionVisible;
        public bool MyCollectionVisible
        {
            get { return _myCollectionVisible; }
            set { SetProperty(ref _myCollectionVisible, value); }
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
        public INavigationService _nav;
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _nav = navigationService;
            HomeIsVisible = true;
            GetAllCollections();
            LogoutCommand = new DelegateCommand(Logout);
            EditCollectionCommand = new DelegateCommand(EditCollection);
            GoBackFromCollectionsCommand = new DelegateCommand(GoBack);
        }
        public void Logout()
        {
            Application.Current.Properties["IsLogedIn"] = false;
            Application.Current.MainPage = new SignInPage();
        }
        public void EditCollection()
        {
           
        }
        public async void GetAllCollections()
        {
            IsBusy = true;
            var response = await new ApiData().GetData<List<CollectionModel>>("Collections/GetAllCollections", true);
            if (response != null)
            {
                AllCollections = new ObservableCollection<CollectionModel>(response.data);               
            }
            IsBusy = false;
        }
        public void GoBack()
        {
            HomeIsVisible = true;
            MyCollectionVisible = false;
        }
    }
}

