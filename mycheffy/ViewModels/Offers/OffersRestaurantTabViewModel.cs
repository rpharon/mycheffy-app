using ImTools;
using mycheffy.Models.FoodItem;
using mycheffy.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using System.Text;


namespace mycheffy.ViewModels.Offers
{
    [DataContract]
    public class OffersRestaurantTabViewModel : NavigationViewModelBase
    {
        [DataMember]
        [ObservableAsProperty]
        public IEnumerable<FoodItemModel> TodayOffers { get; }
        [DataMember]
        [ObservableAsProperty]
        public IEnumerable<FoodItemModel> DiscountList { get; }
        [DataMember]
        [ObservableAsProperty]
        public IEnumerable<FoodItemModel> FreeDelivery { get; }
        [DataMember]
        [ObservableAsProperty]
        public IEnumerable<FoodItemModel> AllOffers { get; }

        OffersRestaurantTabViewModel(FoodService foodService)
        {

            foodService.GetFeaturedProducts().ToObservable().ObserveOn(RxApp.MainThreadScheduler)
           .Select(products =>
           {
               products = products.Map(model =>
               {
                   model.photo = string.IsNullOrEmpty(model.photo) ? "burger" : model.photo;
                   return model;
               });
               return products;
           }).ToPropertyEx(this, x => x.TodayOffers);
            foodService.GetFeaturedProducts().ToObservable().ObserveOn(RxApp.MainThreadScheduler)
            .Select(products =>
            {
                products = products.Map(model =>
                {
                    model.photo = string.IsNullOrEmpty(model.photo) ? "burger" : model.photo;
                    return model;
                });
                return products;
            }).ToPropertyEx(this, x => x.DiscountList);
            foodService.GetFeaturedProducts().ToObservable().ObserveOn(RxApp.MainThreadScheduler)
           .Select(products =>
           {
               products = products.Map(model =>
               {
                   model.photo = string.IsNullOrEmpty(model.photo) ? "burger" : model.photo;
                   return model;
               });
               return products;
           }).ToPropertyEx(this, x => x.FreeDelivery);
            foodService.GetFeaturedProducts().ToObservable().ObserveOn(RxApp.MainThreadScheduler)
            .Select(products =>
            {
                products = products.Map(model =>
                {
                    model.photo = string.IsNullOrEmpty(model.photo) ? "burger" : model.photo;
                    return model;
                });
                return products;
            }).ToPropertyEx(this, x => x.AllOffers);
        }
    }
}
