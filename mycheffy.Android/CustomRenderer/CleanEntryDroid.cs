using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using mycheffy.CustomRenderer;
using mycheffy.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CleanEntry), typeof(CleanEntryDroid))]

namespace mycheffy.Droid.CustomRenderer
{
    internal class CleanEntryDroid : EntryRenderer
    {
        public CleanEntryDroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}