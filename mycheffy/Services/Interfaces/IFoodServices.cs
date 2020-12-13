using mycheffy.Models;
using mycheffy.Models.FoodItem;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mycheffy.Services.Interfaces
{
    [Headers("Accept: application/json")]
    internal interface IFoodServices
    {
        [Get("/products/featured")]
        Task<IEnumerable<FoodItemModel>> GetFeaturedProducts();

        [Get("/products")]
        Task<Pagination<FoodItemModel>> GetAll();
    }
}