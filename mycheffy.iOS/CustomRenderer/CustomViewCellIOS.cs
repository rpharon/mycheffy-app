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

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellIOS))]

namespace mycheffy.iOS.CustomRenderer
{
    public class CustomViewCellIOS : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            var view = item as CustomViewCell;
            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = view.SelectedItemBackgroundColor.ToUIColor(),
            };

            return cell;
        }
    }
}