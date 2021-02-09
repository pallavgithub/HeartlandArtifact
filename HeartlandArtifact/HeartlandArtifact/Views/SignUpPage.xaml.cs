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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }
        private void PasswordEntry_Unfocused(object sender, FocusEventArgs e)
        {
            (BindingContext as SignUpPageViewModel).ShowPassword = false;
        }

        private void PasswordEntry_Focused(object sender, FocusEventArgs e)
        {
            (BindingContext as SignUpPageViewModel).ShowPassword = true;
        }

        private void ConfirmPasswordEntry_Focused(object sender, FocusEventArgs e)
        {
            (BindingContext as SignUpPageViewModel).ShowConfirmPassword = true;

        }

        private void ConfirmPasswordEntry_Unfocused(object sender, FocusEventArgs e)
        {
            (BindingContext as SignUpPageViewModel).ShowConfirmPassword = false;
        }
    }
}