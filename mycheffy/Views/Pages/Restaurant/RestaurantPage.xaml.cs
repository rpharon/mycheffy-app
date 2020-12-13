using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using mycheffy.ViewModels.Restaurant;
using mycheffy.Views.Popups;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace mycheffy.Views.Pages.Restaurant
{
    public partial class RestaurantPage : ContentPageBase<RestaurantViewModel>
    {
        public RestaurantPage()
        {
            InitializeComponent();

            this.BindCommand(ViewModel, x => x.Back, x => x.BackButton)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.Extra, x => x.ExtraButton)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.Offer, x => x.OfferButton)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.Checkout, x => x.CheckoutButton)
                .DisposeWith(ViewBindings);            

            //FoodList.Events().ItemTapped.ObserveOn(RxApp.MainThreadScheduler).Subscribe(async e =>
            //{
            //    ProductListItem item = e.Item as ProductListItem;
            //    await PopupNavigation.PushAsync(new CustomizeOrderPopup(item));
            //}).DisposeWith(ViewBindings);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            Padding = safeInsets;
        }
    }
}
