using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using mycheffy.Models.Restaurant;
using mycheffy.Services.Interfaces;
using mycheffy.Services.Middleware;
using Refit;

namespace mycheffy.Services
{
    public class RestaurantDetailsService : IRestaurantDetailsServices
    {
        private readonly HttpClient _httpClient;
        private readonly IRestaurantDetailsServices restaurantDetailsApi;

        public RestaurantDetailsService()
        {
            _httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler())) { BaseAddress = new Uri(NetworkService.baseUrl) };
            restaurantDetailsApi = RestService.For<IRestaurantDetailsServices>(_httpClient);
        }

        public async Task<RestaurantDetailModel> GetRestaurantDetails(string slug)
        {
            return await restaurantDetailsApi.GetRestaurantDetails(slug).ConfigureAwait(false);
        }
    }
}
