using mycheffy.Models.Category;
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
    public class CategoryService : ICategoryServices
    {
        private readonly HttpClient _httpClient;
        private readonly ICategoryServices _categoryApi;

        private CategoryService()
        {
            _httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler())) { BaseAddress = new Uri(NetworkService.baseUrl) };
            _categoryApi = RestService.For<ICategoryServices>(_httpClient);
        }

        public async Task<IEnumerable<CategoryModel>> GetAll()
        {
            return await _categoryApi.GetAll().ConfigureAwait(false);
        }
    }
}