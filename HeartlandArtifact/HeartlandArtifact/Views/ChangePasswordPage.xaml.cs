using HeartlandArtifact.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartlandArtifact.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed() => true;

        private async void ShowPassword_Tapped(object sender, EventArgs e)
        {
            (BindingContext as ChangePasswordPageViewModel).ShowPassword = !(BindingContext as ChangePasswordPageViewModel).ShowPassword;
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            if (!string.IsNullOrEmpty(PasswordEntry.Text))
            {
                await Task.Delay(10);
                PasswordEntry.CursorPosition = PasswordEntry.Text.Length;
            }
        }
        private async void ShowConfirmPassword_Tapped(object sender, EventArgs e)
        {
            (BindingContext as ChangePasswordPageViewModel).ShowConfirmPassword = !(BindingContext as ChangePasswordPageViewModel).ShowConfirmPassword;
            ConfirmPasswordEntry.IsPassword = !ConfirmPasswordEntry.IsPassword;
            if (!string.IsNullOrEmpty(ConfirmPasswordEntry.Text))
            {
                await Task.Delay(10);
                ConfirmPasswordEntry.CursorPosition = ConfirmPasswordEntry.Text.Length;
            }
        }
    }
}