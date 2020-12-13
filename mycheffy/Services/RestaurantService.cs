using mycheffy.Models;
using mycheffy.Models.Restaurant;
using mycheffy.Services.Interfaces;
using mycheffy.Services.Middleware;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mycheffy.Services
{
    public class RestaurantService : IRestaurantServices
    {
        private readonly HttpClient _httpClient;
        private readonly IRestaurantServices _restaurantAPi;

        private RestaurantService()
        {
            _httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler())) { BaseAddress = new Uri(NetworkService.baseUrl) };
            _restaurantAPi = RestService.For<IRestaurantServices>(_httpClient);
        }

        public async Task<Pagination<RestaurantModel>> GetAll()
        {
            return await _restaurantAPi.GetAll().ConfigureAwait(false);
        }

        public async Task<RestaurantDetailModel> GetRestaurantDetails(string slug)
        {
            return await _restaurantAPi.GetRestaurantDetails(slug).ConfigureAwait(false);
        }
    }
}
