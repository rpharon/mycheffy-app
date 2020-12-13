using mycheffy.CustomRenderer;
using mycheffy.Droid.CustomRenderer;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellDroid))]
namespace mycheffy.Droid.CustomRenderer
{
    internal class CustomViewCellDroid : ViewCellRenderer
    {
        private Android.Views.View _cellCore;
        private Android.Widget.ListView _listView;
        private Drawable _unselectedBackground;
        private Android.Graphics.Color _defaultAmbientShadowColor;
        private Android.Graphics.Color _defaultOutlineSpotShadowColor;
        private bool _selected;

        protected override Android.Views.View GetCellCore(Cell item,
                                                          Android.Views.View convertView,
                                                          ViewGroup parent,
                                                          Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);
            _listView = parent as Android.Widget.ListView;

            _selected = false;
            _unselectedBackground = _cellCore.Background;
            _defaultAmbientShadowColor = new Android.Graphics.Color(_cellCore.OutlineAmbientShadowColor);
            _defaultOutlineSpotShadowColor = new Android.Graphics.Color(_cellCore.OutlineSpotShadowColor);
            return _cellCore;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnCellPropertyChanged(sender, args);

            if (args.PropertyName == "IsSelected")
            {
                _selected = !_selected;

                if (_selected)
                {
                    var extendedViewCell = sender as CustomViewCell;
                    _cellCore.SetBackgroundColor(extendedViewCell.SelectedItemBackgroundColor.ToAndroid());
                    _cellCore.SetOutlineAmbientShadowColor(Android.Graphics.Color.LightGray);
                    _cellCore.SetOutlineSpotShadowColor(Android.Graphics.Color.LightGray);
                }
                else
                {
                    _cellCore.SetBackground(_unselectedBackground);
                    _cellCore.SetOutlineAmbientShadowColor(_defaultAmbientShadowColor);
                    _cellCore.SetOutlineSpotShadowColor(_defaultOutlineSpotShadowColor);
                }
            }
        }
    }
}