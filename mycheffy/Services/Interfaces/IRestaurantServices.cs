using mycheffy.Models;
using mycheffy.Models.Category;
using mycheffy.Models.Restaurant;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mycheffy.Services.Interfaces
{
    [Headers("Accept: application/json")]
    public interface IRestaurantServices
    {
        [Get("/restaurants")]
        Task<Pagination<RestaurantModel>> GetAll();

        [Get("/restaurants/{slug}")]
        Task<RestaurantDetailModel> GetRestaurantDetails(string slug);
    }
}
