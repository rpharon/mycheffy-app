using ImTools;
using mycheffy.Models.FoodItem;
using mycheffy.Services;
using mycheffy.Views.ContentViews.Offers;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Unit = System.Reactive.Unit;

namespace mycheffy.ViewModels.Offers
{
    [DataContract]
    public class OffersViewModel : NavigationViewModelBase
    {
        [DataMember]
        [Reactive] public ContentView Content { get; set; }

        public ReactiveCommand<Unit, Unit> ChangeRestaurant { get; set; }
        public ReactiveCommand<Unit, Unit> ChangePromo { get; set; }

        OffersRestaurantTabContentView restaurantView = new OffersRestaurantTabContentView();
        OffersPromoTabContentView offerView = new OffersPromoTabContentView();
        private bool currView = true;

        public OffersViewModel(FoodService foodService)
        {
            Content = new OffersRestaurantTabContentView();

            ChangeRestaurant = ReactiveCommand.Create(() =>
            {
                if (!currView)
                {
                    currView = true;
                    Content = restaurantView;
                }
            });

            ChangePromo = ReactiveCommand.Create(() =>
            {
                if (currView)
                {
                    currView = false;
                    Content = offerView;
                }
            });
        }

    }
}
