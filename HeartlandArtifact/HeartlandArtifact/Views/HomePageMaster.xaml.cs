﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageMaster : ContentPage
    {
        public ListView ListView;
        public StackLayout Logout_Option;
        public Image Menu_Icon;
        public HomePageMaster()
        {
            InitializeComponent();

            BindingContext = new HomePageMasterViewModel();
            ListView = MenuItemsListView;
            Logout_Option = LogoutOption;
            Menu_Icon = MasterMenuIcon;
        }
        protected override bool OnBackButtonPressed() => true;

        class HomePageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<HomePageMasterMenuItem> MenuItems { get; set; }

            public HomePageMasterViewModel()
            {
                MenuItems = new ObservableCollection<HomePageMasterMenuItem>(new[]
                {
                    new HomePageMasterMenuItem { Id = 0, Title = "Home", IconImage="HomeIcon.png"},
                    new HomePageMasterMenuItem { Id = 1, Title = "Sold Items", IconImage="Dollar.png" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        private async void Logout_Tapped(object sender, EventArgs e)
        {
            Application.Current.Properties["IsLogedIn"] = false;
            await Application.Current.SavePropertiesAsync();
            App.Current.MainPage = new NavigationPage(new SignInPage());

        }
    }
}