using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using HeartlandArtifact.CustomRenderers;
using HeartlandArtifact.Droid.CustomRenderer;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(BorderLessEntry), typeof(CustomEntryRenderer))]
namespace HeartlandArtifact.Droid.CustomRenderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Rgb(255, 245, 246));
                Control.SetPadding(20, 0, 20, 0);
                Control.Gravity = GravityFlags.CenterVertical;
                TextAlignment = Android.Views.TextAlignment.Center;
                Control.TextAlignment = Android.Views.TextAlignment.Center;
                GradientDrawable gd = new GradientDrawable();
                gd.SetCornerRadius(5);
                this.Control.SetBackground(gd);
            }
        }

        public override bool DispatchKeyEvent(KeyEvent e)
        {
            if (e.Action == KeyEventActions.Down)
            {
                if (e.KeyCode == Keycode.Del)
                {
                    if (string.IsNullOrWhiteSpace(Control.Text))
                    {
                        var entry = (BorderLessEntry)Element;
                        entry.OnBackSpacePressed();
                    }
                }
            }
            return base.DispatchKeyEvent(e);
        }
    }
}