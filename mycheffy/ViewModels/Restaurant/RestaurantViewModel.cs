using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using ImTools;
using mycheffy.Models.CartList;
using mycheffy.Models.FoodItem;
using mycheffy.Models.Restaurant;
using mycheffy.Models.Shared.Enums;
using mycheffy.Services;
using mycheffy.Views.Popups;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Unit = System.Reactive.Unit;

namespace mycheffy.ViewModels.Restaurant
{
    public class ProductListItem
    {
        public FoodItemModel product { get; set; }
        public int quantity { get; set; }
        public ProductListItem()
        {
        }
    }

    public class RestaurantViewModel : NavigationViewModelBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        RestaurantDetailsService _restaurantDetailsService = new RestaurantDetailsService();

        [Reactive]
        public string Name { get; set; }
        [Reactive]
        public string Slug { get; set; }
        [Reactive]
        public double Rating { get; set; }
        [Reactive]
        public int TotalReviews { get; set; }
        [Reactive]
        public bool IsRunning { get; set; }
        [Reactive]
        public int TotalQuantity { get; set; }
        [Reactive]
        public float TotalPrice { get; set; }

        public IDisposable subscription, subscription_2;
        public ReactiveCommand<Unit, Unit> Back { get; set; }
        public ReactiveCommand<Unit, Unit> Extra { get; set; }
        public ReactiveCommand<Unit, Unit> Offer { get; set; }
        public ReactiveCommand<Unit, Unit> Checkout { get; set; }

        [Reactive]
        public IEnumerable<ProductListItem> Products { get; set; }

        private RestaurantService _restaurantService;
        private CartService _cartService;

        public RestaurantViewModel(INavigationService navigationService, RestaurantService restaurantService, CartService cartService)
        {
            Console.WriteLine("RestaurantViewModel Constructor");

            IsRunning = true;

            _navigationService = navigationService;
            _restaurantService = restaurantService;
            _cartService = cartService;

            TotalQuantity = 0;
            TotalPrice = 0;

            Back = ReactiveCommand.CreateFromTask(ExecuteBack);
            Extra = ReactiveCommand.CreateFromTask(ExecuteExtra);
            Offer = ReactiveCommand.CreateFromTask(ExecuteOffer);
            Checkout = ReactiveCommand.CreateFromTask(ExecuteCheckout);
        }

        private async Task ExecuteBack()
        {
            await _navigationService.GoBackAsync();
        }

        private async Task ExecuteExtra()
        {
            await Application.Current.MainPage.DisplayAlert("Restaurant", "Action not available", "OK");
        }

        private async Task ExecuteOffer()
        {
            await Application.Current.MainPage.DisplayAlert("Restaurant", "Action not available", "OK");
        }

        private async Task ExecuteCheckout()
        {
            await Application.Current.MainPage.DisplayAlert("Restaurant", "Action not available", "OK");
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            subscription.Dispose();
            subscription_2.Dispose();
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Slug = parameters.GetValue<string>("data");
            IsRunning = true;

            await PopupNavigation.Instance.PushAsync(new PopupActivityIndicator());
            subscription_2 = Observable.CombineLatest(_restaurantService.GetRestaurantDetails(Slug).ToObservable(), _cartService.GetAll().ToObservable(), (restaurant, orders) =>
            {
                Name = restaurant.name;
                //Calculate score for rating
                double score = 0;
                double count = 0;
                TotalReviews = restaurant.reviews.Count();
                foreach (var review in restaurant.reviews)
                {
                    score += review.rating;
                    count++;
                }
                if (count != 0)
                {
                    Rating = Math.Round(score / count, 1);
                }
                else
                {
                    Rating = 0;
                }

                IEnumerable<ProductListItem> list = restaurant.products.ToList().Map(product =>
                {
                    ProductListItem item = new ProductListItem();
                    item.product = product;
                    CartItemModel cartItem = orders.Content.Where(order => order.id == product.id).FirstOrDefault();
                    item.quantity = cartItem != null ? cartItem.quantity : 0;

                    return item;
                });

                return list;
            }).Finally(async () =>
            {
                IsRunning = false;
                await PopupNavigation.Instance.PopAsync();
            }).Subscribe(productList =>
            {
                Products = productList;
                CalculateQuantityAndTotalPrice();
            });

            subscription = MessageBus.Current.Listen<RestaurantEvents>().Select(resEvent =>
            {
                IEnumerable<ProductListItem> list = Products;
                if (resEvent.action == EventActions.Add)
                {
                    IEnumerable<CartItemModel> cartList = (IEnumerable<CartItemModel>)resEvent.data;
                    list = list.Map(productListItem =>
                    {
                        CartItemModel item = cartList.FindFirst(cartItem => cartItem.id == productListItem.product.id);
                        if (item != null)
                        {
                            productListItem.quantity = item.quantity;
                        }

                        return productListItem;
                    });
                }
                else if (resEvent.action == EventActions.Update)
                {
                    CartItemModel cartItem = (CartItemModel)resEvent.data;
                    list = list.Map(product =>
                    {
                        if (product.product.id == cartItem.id)
                        {
                            product.quantity = cartItem.quantity;
                        }
                        return product;
                    });
                }
                CalculateQuantityAndTotalPrice();
                return list;
            }).Subscribe(productList =>
            {
                Products = productList;
            });
        }

        public void OnNavigating(INavigationParameters paramaters)
        {
        }

        private void CalculateQuantityAndTotalPrice()
        {
            int totalQuantity = 0;
            float totalPrice = 0;
            Products.ToList().ForEach(productItem =>
            {
                totalQuantity += productItem.quantity;
                totalPrice += (float)(productItem.quantity * productItem.product.price);
            });
            TotalPrice = totalPrice;
            TotalQuantity = totalQuantity;
        }
    }
}
