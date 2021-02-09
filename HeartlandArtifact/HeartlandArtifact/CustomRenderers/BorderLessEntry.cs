using System;
using Xamarin.Forms;

namespace HeartlandArtifact.CustomRenderers
{
    public class BorderLessEntry : Entry
    {
        public event Action<object, EventArgs> OnBackspace;
        public void OnBackSpacePressed()
        {
            if (OnBackspace != null)
            {
                OnBackspace.Invoke(this, null);
            }
        }

        /// <summary>
        /// Added for removing padding from entry field on enter otp page for iOS.
        /// True only for enter otp page on iOS.
        /// </summary>
        public static readonly BindableProperty RemovePaddingProperty =
        BindableProperty.Create(nameof(RemovePadding), typeof(bool), typeof(BorderLessEntry), false, BindingMode.TwoWay);
        public bool RemovePadding
        {
            get { return (bool)GetValue(RemovePaddingProperty); }
            set { SetValue(RemovePaddingProperty, value); }
        }

    }
}
