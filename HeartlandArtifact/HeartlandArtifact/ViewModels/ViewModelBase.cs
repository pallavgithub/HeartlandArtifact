using HeartlandArtifact.Interfaces;
using HeartlandArtifact.Services.Contracts;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HeartlandArtifact.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        private bool _isNotConnected;
        public bool IsNotConnected
        {
            get { return _isNotConnected; }
            set { SetProperty(ref _isNotConnected, value); }
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase()
        {

        }
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
        public ViewModelBase(IFacebookManager facebookManager, IGoogleManager googleManager, INavigationService navigationService)
        {
            NavigationService = navigationService; 
            Connectivity.ConnectivityChanged += Internet_ConnectionChanged;
            IsNotConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
        ~ViewModelBase()
        {
            Connectivity.ConnectivityChanged -= Internet_ConnectionChanged;
        }
        void Internet_ConnectionChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var toast = DependencyService.Get<IMessage>();
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                //Application.Current.MainPage.DisplayAlert("Alert", "No Internet Connection", "OK");
                toast.LongAlert("No Internet Connection");
            }
            else
            {
                //Application.Current.MainPage.DisplayAlert("Alert", "Your Internet Connection is Back", "OK");
                toast.LongAlert("Your Internet Connection is Back");
            }
        }
    }
}
