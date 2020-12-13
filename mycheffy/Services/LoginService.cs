using System;
using System.Net.Http;
using System.Threading.Tasks;
using mycheffy.Models.Login;
using mycheffy.Services.Interfaces;
using mycheffy.Services.Middleware;
using Newtonsoft.Json.Linq;
using Refit;

namespace mycheffy.Services
{
    public class LoginService : ILoginServices
    {
        private readonly HttpClient _httpClient;
        private readonly ILoginServices _loginApi;

        public LoginService()
        {
            _httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler())) { BaseAddress = new Uri(NetworkService.baseUrl) };
            _loginApi = RestService.For<ILoginServices>(_httpClient);
        }

        public async Task<ApiResponse<LoginModel>> SignIn(string grant_type, string client_id, string client_secret, string username, string password, string scope, string type, string provider, string name, string photo)
        {
            var response = await _loginApi.SignIn(grant_type, client_id, client_secret, username, password, scope, type, provider, name, photo);

            return response;
        }
    }
}
