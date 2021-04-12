using HeartlandArtifact.ViewModels;
using System;
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
        protected override bool OnBackButtonPressed() => false;

        //private void PasswordEntry_Unfocused(object sender, FocusEventArgs e)
        //{
        //    // if (string.IsNullOrEmpty(PasswordEntry.Text))
        //    //  (BindingContext as SignInPageViewModel).ShowPassword = false;
        //}

        //private void PasswordEntry_Focused(object sender, FocusEventArgs e)
        //{
        //    //(BindingContext as SignInPageViewModel).ShowPassword = true;
        //}

        private async void ShowPassword_Tapped(object sender, EventArgs e)
        {
            (BindingContext as SignInPageViewModel).ShowPassword = !(BindingContext as SignInPageViewModel).ShowPassword;
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            if (!string.IsNullOrEmpty(PasswordEntry.Text))
            {
                await Task.Delay(10);
                PasswordEntry.CursorPosition = PasswordEntry.Text.Length;
            }
        }
    }
}