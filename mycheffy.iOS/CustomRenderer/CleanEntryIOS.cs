using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using mycheffy.CustomRenderer;
using mycheffy.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CleanEntry), typeof(CleanEntryIOS))]

namespace mycheffy.iOS.CustomRenderer
{
    internal class CleanEntryIOS : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
        }
    }
}