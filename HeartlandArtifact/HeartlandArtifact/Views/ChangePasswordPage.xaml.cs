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
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }
        private void PasswordEntry_Unfocused(object sender, FocusEventArgs e)
        {
            (BindingContext as ChangePasswordPageViewModel).ShowPassword = false;
        }

        private void PasswordEntry_Focused(object sender, FocusEventArgs e)
        {
            (BindingContext as ChangePasswordPageViewModel).ShowPassword = true;
        }
        private void ConfirmPasswordEntry_Unfocused(object sender, FocusEventArgs e)
        {
            (BindingContext as ChangePasswordPageViewModel).ShowConfirmPassword = false;
        }

        private void ConfirmPasswordEntry_Focused(object sender, FocusEventArgs e)
        {
            (BindingContext as ChangePasswordPageViewModel).ShowConfirmPassword = true;
        }
    }
}