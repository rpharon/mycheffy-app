using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using mycheffy.ViewModels.Offers;
using ReactiveUI;
using Xamarin.Forms;

namespace mycheffy.Views.ContentViews.Offers
{
    public partial class OffersContentView : ContentViewBase<OffersViewModel>
    {
        OffersRestaurantTabContentView restaurantView = new OffersRestaurantTabContentView();
        OffersPromoTabContentView offerView = new OffersPromoTabContentView();

        public OffersContentView()
        {
            InitializeComponent();

            this.BindCommand(ViewModel, x => x.ChangeRestaurant, x => x.RestaurantTab)
            .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.ChangePromo, x => x.PromoTab)
            .DisposeWith(ViewBindings);

            //RestaurantTab.Events().Clicked.ObserveOn(RxApp.MainThreadScheduler).Subscribe(_ =>
            //{
            //    ChangeTab(new Thickness(0, 10, 0, 20), new CornerRadius(0, 0, 0, 0), restaurantView, RestaurantTab, PromoTab, RestaurantButtonContainer, PromoButtonContainer, true);
            //}).DisposeWith(ViewBindings);
            //PromoTab.Events().Clicked.ObserveOn(RxApp.MainThreadScheduler).Subscribe(_ =>
            //{
            //    ChangeTab(new Thickness(0, 10, 0, 20), new CornerRadius(0, 0, 24, 24), offerView, PromoTab, RestaurantTab, PromoButtonContainer, RestaurantButtonContainer, false);
            //}).DisposeWith(ViewBindings);
        }

        private void ChangeTab(Thickness padding, CornerRadius cornerRadius, ContentView view, Button current, Button other, StackLayout currContainer, StackLayout otherContainer, bool isRestaurant)
        {
            Color buttonColor = (Color)GetResourceValue("ButtonColor");
            TopBar.Padding = padding;
            TopBar.CornerRadius = cornerRadius;
            current.TextColor = buttonColor;
            currContainer.BackgroundColor = (Color)GetResourceValue("PrimaryBackgroundColor");

            other.TextColor = Color.White;
            otherContainer.BackgroundColor = buttonColor;
        }

        private object GetResourceValue(string keyName)
        {
            // Search all dictionaries
            if (Application.Current.Resources.TryGetValue(keyName, out var retVal)) { }
            return retVal;
        }
    }
}
