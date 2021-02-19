using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageDetail : ContentPage
    {
        public HomePageDetail()
        {
            InitializeComponent();
        }
        //private void Menu_Tapped(object sender, EventArgs e)
        //{
        //    (DetailStack.Parent.Parent.Parent.Parent).IsPresented = true;
        //}
    }
}