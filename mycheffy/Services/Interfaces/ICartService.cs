using mycheffy.Models.CartList;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mycheffy.Services.Interfaces
{
    [Headers("Accept: application/json")]
    internal interface ICartService
    {
        [Get("/cart")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<IEnumerable<CartItemModel>>> GetAll();

        [Put("/cart")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<CartItemModel>> UpdateCartItem(string product_id, int quantity, float price);

        [Post("/cart")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<IEnumerable<CartItemModel>>> AddCartItem(string product_id, int quantity, float price);

        [Delete("/cart/{product_id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<bool>> DeleteCartItem(string product_id);

        [Delete("/cart")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<bool>> ClearCart();
    }
}
