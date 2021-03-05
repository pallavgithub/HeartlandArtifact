using HeartlandArtifact.Helpers;
using HeartlandArtifact.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HeartlandArtifact.ViewModels
{
    public class MyCollectionUserControlViewModel : ViewModelBase
    {
        public HomePageViewModel HomePageViewModel { get; set; }
        public DelegateCommand EditCollectionCommand { get; set; }
        public DelegateCommand GoBackFromCollectionsCommand { get; set; }
        private ObservableCollection<CollectionModel> _allCollections;
        public ObservableCollection<CollectionModel> AllCollections
        {
            get { return _allCollections; }
            set { SetProperty(ref _allCollections, value); }
        }
        public MyCollectionUserControlViewModel(INavigationService navigationService) : base(navigationService)
        {
            GetAllCollections();
            EditCollectionCommand = new DelegateCommand(EditCollection);
            GoBackFromCollectionsCommand = new DelegateCommand(GoBack);
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
            HomePageViewModel.HomeIsVisible = true;
            HomePageViewModel.MyCollectionVisible = false;
        }
    }
}
