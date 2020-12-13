using RxUI.Plugins.Popup;
using System;
using System.Collections.Generic;
using System.Text;

namespace mycheffy.Views.Popups
{
    public class PopUpBase<T> : ReactivePopupPage<T>
        where T : class
    {
        protected PopUpBase()
        {
            SystemPaddingSides = Rg.Plugins.Popup.Enums.PaddingSide.All;
        }
    }
}
