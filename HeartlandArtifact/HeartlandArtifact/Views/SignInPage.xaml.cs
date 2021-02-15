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
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed() => true;

        private void PasswordEntry_Unfocused(object sender, FocusEventArgs e)
        {
            (BindingContext as SignInPageViewModel).ShowPassword = false;
        }

        private void PasswordEntry_Focused(object sender, FocusEventArgs e)
        {
            (BindingContext as SignInPageViewModel).ShowPassword = true;
        }
    }
}