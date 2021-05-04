using Android.Graphics.Drawables;
using HeartlandArtifact.CustomRenderers;
using HeartlandArtifact.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderLessEditor), typeof(CustomEditorRenderer))]
namespace HeartlandArtifact.Droid.CustomRenderer
{
    [System.Obsolete]
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
}