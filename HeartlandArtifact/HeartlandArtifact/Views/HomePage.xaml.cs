using HeartlandArtifact.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : MasterDetailPage
    {
        public HomePage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as HomePageMasterMenuItem;
            if (item == null)
                return;

            // var page = (Page)Activator.CreateInstance(item.TargetType);
            // page.Title = item.Title;

            // Detail = new NavigationPage(page);
            if(item.Title=="Home")
            {
                (BindingContext as HomePageViewModel).SoldItemsIsVisible = false;
                (BindingContext as HomePageViewModel).HomeIsVisible = true;
            }
            if (item.Title == "Sold Items")
            {
                (BindingContext as HomePageViewModel).HomeIsVisible = false;
                (BindingContext as HomePageViewModel).SoldItemsIsVisible = true;
            }
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
        private void Menu_Tapped(object sender, EventArgs e)
        {
            IsPresented = true;
        }
    }
}