using HeartlandArtifact.CustomRenderers;
using HeartlandArtifact.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderLessEditor), typeof(CustomEditorRenderer))]
namespace HeartlandArtifact.iOS.CustomRenderer
{
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.BackgroundColor = UIColor.FromWhiteAlpha(1, (System.nfloat)0.5);
                Control.Layer.CornerRadius = 5;                
            }
        }
    }
}