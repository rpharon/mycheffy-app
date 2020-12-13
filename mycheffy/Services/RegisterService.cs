using System;
using System.Net.Http;
using System.Threading.Tasks;
using mycheffy.Models.Register;
using mycheffy.Services.Interfaces;
using mycheffy.Services.Middleware;
using Refit;

namespace mycheffy.Services
{
    public class RegisterService : IRegisterServices
    {
        private readonly HttpClient _httpClient;
        private readonly IRegisterServices _registerApi;

        public RegisterService()
        {
            _httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler())) { BaseAddress = new Uri(NetworkService.baseUrl) };
            _registerApi = RestService.For<IRegisterServices>(_httpClient);
        }

        public async Task<ApiResponse<RegisterModel>> Register(string first_name, string last_name, string email, string password, string password_confirmation, string type, string sns, string sns_id, string photo, int is_vendor)
        {
            var response = await _registerApi.Register(first_name, last_name, email, password, password_confirmation, type, sns, sns_id, photo, is_vendor);

            return response;
        }
    }
}
