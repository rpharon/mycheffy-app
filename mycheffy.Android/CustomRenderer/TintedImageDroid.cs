using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using System.ComponentModel;
using mycheffy.CustomRenderer;
using mycheffy.Droid.CustomRenderer;
using Android.Content;

[assembly: ExportRenderer(typeof(TintedImage), typeof(TintedImageDroid))]
namespace mycheffy.Droid.CustomRenderer
{
    internal class TintedImageDroid : ImageRenderer
    {
        public TintedImageDroid(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            SetTint();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == TintedImage.TintColorProperty.PropertyName || e.PropertyName == TintedImage.SourceProperty.PropertyName)
                SetTint();
        }

        void SetTint()
        {
            if (Control == null || Element == null)
                return;

            if (((TintedImage)Element).TintColor.Equals(Xamarin.Forms.Color.Transparent))
            {
                //Turn off tinting

                if (Control.ColorFilter != null)
                    Control.ClearColorFilter();

                return;
            }

            //Apply tint color
            var colorFilter = new PorterDuffColorFilter(((TintedImage)Element).TintColor.ToAndroid(), PorterDuff.Mode.SrcIn);
            Control.SetColorFilter(colorFilter);
        }
    }
}