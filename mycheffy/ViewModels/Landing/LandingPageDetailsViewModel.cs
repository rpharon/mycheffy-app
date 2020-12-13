using ImTools;
using mycheffy.Models.Category;
using mycheffy.Models.FoodItem;
using mycheffy.Models.Restaurant;
using mycheffy.Services;
using mycheffy.Services.Utilities;
using mycheffy.Views.Pages.Restaurant;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace mycheffy.ViewModels.Landing
{
    public class LandingPageDetailsViewModel : NavigationViewModelBase
    {
        NavigationEventService _navigationService;

        [ObservableAsProperty]
        public IEnumerable<CategoryModel> CategoryList { get; }

        [ObservableAsProperty]
        public IEnumerable<FoodItemModel> FeaturedProductList { get; }

        [ObservableAsProperty]
        public IEnumerable<RestaurantModel> NearbyRestaurants { get; }

        public LandingPageDetailsViewModel(CategoryService categoryService, FoodService foodService, RestaurantService restaurantService, NavigationEventService navigationService)
        {
            _navigationService = navigationService;
            categoryService.GetAll().ToObservable()
                .Select(categories =>
                {
                    categories = categories.Map(model =>
                    {
                        model.photo = string.IsNullOrEmpty(model.photo) ? "burger" : model.photo;
                        return model;
                    });
                    return categories;
                }).ToPropertyEx(this, x => x.CategoryList);

            foodService.GetAll().ToObservable()
                .Select(products =>
                {
                    return products.data.Map(model =>
                    {
                        model.photo = string.IsNullOrEmpty(model.photo) ? "burger" : model.photo;
                        return model;
                    });
                }).ToPropertyEx(this, x => x.FeaturedProductList);

            restaurantService.GetAll().ToObservable()
                .Select(pagination =>
                {
                    IEnumerable<RestaurantModel> restaurants = pagination.data.Map(model =>
                    {
                        model.photo = string.IsNullOrEmpty(model.photo) ? "burger" : model.photo;
                        return model;
                    });
                    return restaurants;
                }).ToPropertyEx(this, x => x.NearbyRestaurants);

        }

        public void NavigateToRestaurantAsync(Object data, Type type)
        {
            Console.WriteLine("NavigateToRestaurantAsync");

            Console.WriteLine("Slug: " + JsonConvert.SerializeObject(data));

            _navigationService.PushToStack("RestaurantPage", data, type);
        }
    }
}