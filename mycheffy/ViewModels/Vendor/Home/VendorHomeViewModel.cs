using ImTools;
using mycheffy.Models.Review;
using mycheffy.Services;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace mycheffy.ViewModels.Vendor.Home
{
    public class VendorHomeViewModel : NavigationViewModelBase
    {
        [ObservableAsProperty]
        public IEnumerable<RestaurantReviewModel> Reviews { get; }

        VendorHomeViewModel(RestaurantService restaurantService)
        {
            restaurantService.GetAll().ToObservable()
              .Select(pagination =>
              {
                  IEnumerable<RestaurantReviewModel> restaurants = pagination.data.Aggregate(new List<RestaurantReviewModel>(), (acc, model) => {
                      acc.AddRange(model.reviews.Map(review => {
                          if (String.IsNullOrEmpty(review.user.photo))
                          {
                              review.user.photo = "review_default";
                          }
                          return review;
                      }));
                      return acc;
                  });
                  return restaurants;
              }).ToPropertyEx(this, x => x.Reviews);
        }
    }
}
