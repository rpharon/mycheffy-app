using mycheffy.Models.FoodItem;
using mycheffy.Services;
using mycheffy.Services.Utilities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mycheffy.ViewModels.Landing
{
    public class SearchListViewModel : NavigationViewModelBase
    {
        [ObservableAsProperty]
        public IEnumerable<FoodItemModel> SearchResults { get; }

        [Reactive] public string SearchText { get; set; }

        private FoodService _foodService;

        private NavigationEventService _navigationService;

        public SearchListViewModel(FoodService foodService, NavigationEventService navigationService)
        {
            _foodService = foodService;
            _navigationService = navigationService;
            MessageBus.Current.Listen<string>("search_text")
                .SelectMany(SearchFoodAndRestaurant)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.SearchResults);
        }

        private async Task<IEnumerable<FoodItemModel>> SearchFoodAndRestaurant(string term, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return Enumerable.Empty<FoodItemModel>();
            }
            return await _foodService.SearchItem();
        }

        public void NavigateToRestaurantAsync(string slug)
        {
            Console.WriteLine("NavigateToRestaurantAsync");

            _navigationService.PushToStack("RestaurantPage", slug, typeof(string));
        }
    }
}