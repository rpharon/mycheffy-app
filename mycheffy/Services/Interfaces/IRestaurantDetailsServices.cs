using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mycheffy.Models.Restaurant;
using Refit;

namespace mycheffy.Services.Interfaces
{
    [Headers("Accept: application/json")]
    internal interface IRestaurantDetailsServices
    {
        [Get("/restaurants/{slug}")]
        Task<RestaurantDetailModel> GetRestaurantDetails(string slug);
    }
}
