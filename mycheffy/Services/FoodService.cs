using mycheffy.Models;
using mycheffy.Models.FoodItem;
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
    public class FoodService : IFoodServices
    {
        private readonly HttpClient _httpClient;
        private readonly IFoodServices _foodApi;

        private FoodService()
        {
            _httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler())) { BaseAddress = new Uri(NetworkService.baseUrl) };
            _foodApi = RestService.For<IFoodServices>(_httpClient);
        }

        public async Task<Pagination<FoodItemModel>> GetAll()
        {
            return await _foodApi.GetAll().ConfigureAwait(false);
        }

        public async Task<IEnumerable<FoodItemModel>> GetFeaturedProducts()
        {
            return await _foodApi.GetFeaturedProducts().ConfigureAwait(false);
        }

        public async Task<IEnumerable<FoodItemModel>> SearchItem()
        {
            return await _foodApi.GetFeaturedProducts().ConfigureAwait(false);
        }
    }
}