using mycheffy.Models.CartList;
using mycheffy.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Refit;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace mycheffy.ViewModels.Popup
{
    public class CustomizeOrderViewModel : ViewModelBase
    {
        [Reactive]
        public float TotalPrice { get; set; }
        [Reactive]
        public int Quanitity { get; set; }


        private CartService _cartService;

        public CustomizeOrderViewModel(CartService cartService)
        {
            _cartService = cartService;
            TotalPrice = 0;
            Quanitity = 0;
        }

        public async Task<ApiResponse<IEnumerable<CartItemModel>>> AddCartItem(string product_id, float price)
        {
            return await _cartService.AddCartItem(product_id, Quanitity, price);

        }
        public async Task<ApiResponse<CartItemModel>> UpdateCartItem(string product_id, float price)
        {
            return await _cartService.UpdateCartItem(product_id, Quanitity, price);
        }

    }
}
