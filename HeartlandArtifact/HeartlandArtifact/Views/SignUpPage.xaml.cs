using HeartlandArtifact.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }
       // protected override bool OnBackButtonPressed() => false;
        private async void ShowPassword_Tapped(object sender, EventArgs e)
        {
            (BindingContext as SignUpPageViewModel).ShowPassword = !(BindingContext as SignUpPageViewModel).ShowPassword;
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            if (!string.IsNullOrEmpty(PasswordEntry.Text))
            {
                await Task.Delay(10);
                PasswordEntry.CursorPosition = PasswordEntry.Text.Length;
            }
        }
        private async void ShowConfirmPassword_Tapped(object sender, EventArgs e)
        {
            (BindingContext as SignUpPageViewModel).ShowConfirmPassword = !(BindingContext as SignUpPageViewModel).ShowConfirmPassword;
            ConfirmPasswordEntry.IsPassword = !ConfirmPasswordEntry.IsPassword;
            if (!string.IsNullOrEmpty(ConfirmPasswordEntry.Text))
            {
                await Task.Delay(10);
                ConfirmPasswordEntry.CursorPosition = ConfirmPasswordEntry.Text.Length;
            }
        }
    }
}
