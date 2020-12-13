using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace mycheffy.CustomRenderer
{
    public class TintedImage : Image
    {
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(TintedImage), Color.Black);

        public Color TintColor
        {
            get { return (Color)GetValue(TintColorProperty); }
            set { SetValue(TintColorProperty, value); }
        }
    }
}
