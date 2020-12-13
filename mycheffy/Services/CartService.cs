using mycheffy.Models.CartList;
using mycheffy.Models.Shared.Enums;
using mycheffy.Services.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace mycheffy.Services
{

    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        private ICartService _cartService;

        private async Task<string> GetToken()
        {
            var token = Preferences.Get(PreferencesKey.AccessToken.ToString(), string.Empty);
            Console.WriteLine("Profile Services GetToken: " + token);

            return token;
        }
        public CartService()
        {
            var authenticatedHandler = new AuthenticatedHandler(GetToken) { InnerHandler = new HttpClientHandler() };
            _httpClient = new HttpClient(authenticatedHandler) { BaseAddress = new Uri(NetworkService.baseUrl) };

            _cartService = RestService.For<ICartService>(_httpClient);
        }

        public Task<ApiResponse<IEnumerable<CartItemModel>>> GetAll()
        {
            return _cartService.GetAll();
        }

        public Task<ApiResponse<CartItemModel>> UpdateCartItem(string product_id, int quantity, float price)
        {
            return _cartService.UpdateCartItem(product_id, quantity, price);
        }

        public Task<ApiResponse<IEnumerable<CartItemModel>>> AddCartItem(string product_id, int quantity, float price)
        {
            return _cartService.AddCartItem(product_id, quantity, price);
        }

        public Task<ApiResponse<bool>> DeleteCartItem(string product_id)
        {
            return _cartService.DeleteCartItem(product_id);
        }

        public Task<ApiResponse<bool>> ClearCart()
        {
            return _cartService.ClearCart();
        }
    }
}
