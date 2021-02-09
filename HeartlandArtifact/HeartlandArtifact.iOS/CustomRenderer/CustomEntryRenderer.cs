using CoreGraphics;
using HeartlandArtifact.CustomRenderers;
using HeartlandArtifact.iOS.CustomRenderer;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderLessEntry), typeof(CustomEntryRenderer))]
namespace HeartlandArtifact.iOS.CustomRenderer
{
    class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                var blurEntry = (BorderLessEntry)Element;
                Control.VerticalAlignment = UIControlContentVerticalAlignment.Center; //Control: UiTextField
                /// Control.BackgroundColor = UIColor.FromRGB(252, 243, 237);
                Control.BorderStyle = UITextBorderStyle.None;

                if (blurEntry != null && blurEntry.RemovePadding)
                {
                    Control.LeftView = new UIView(new CGRect(0, 0, 0, 0));
                }
                else
                {
                    Control.LeftView = new UIView(new CGRect(0, 0, 15, 0));
                }
                Control.LeftViewMode = UITextFieldViewMode.Always;
                if (blurEntry != null && blurEntry.RemovePadding)
                {
                    Control.RightView = new UIView(new CGRect(0, 0, 0, 0));
                }
                else
                {
                    Control.RightView = new UIView(new CGRect(0, 0, 15, 0));
                }
                Control.RightViewMode = UITextFieldViewMode.Always;
                Control.Layer.CornerRadius = 5;
            }
        }
    }
}