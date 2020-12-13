using mycheffy.Models.Profile;
using mycheffy.Models.Shared.Enums;
using mycheffy.Services;
using mycheffy.Services.Interfaces;
using mycheffy.Services.Middleware;
using Refit;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace mycheffy.Service
{
    public class ProfileService : IProfileServices
    {
        private readonly HttpClient _httpClient;
        private IProfileServices _profileServices;

        private async Task<string> GetToken()
        {
            var token = Preferences.Get(PreferencesKey.AccessToken.ToString(), string.Empty);
            Console.WriteLine("Profile Services GetToken: " + token);

            return token;
        }

        public ProfileService()
        {
            var authenticatedHandler = new AuthenticatedHandler(GetToken) { InnerHandler = new HttpClientHandler() };
            _httpClient = new HttpClient(authenticatedHandler) { BaseAddress = new Uri(NetworkService.baseUrl) };

            _profileServices = RestService.For<IProfileServices>(_httpClient);

            //_profileServices = RestService.For<IProfileServices>(new HttpClient(new AuthenticatedHttpClientHandler(SecureStorage.GetAsync(SecureStorageKey.OAuthToken.ToString()))) { BaseAddress = new Uri(NetworkService.baseUrl) });
        }

        public async Task<ApiResponse<ProfileModel>> GetCustomerProfile()
        {

            return await _profileServices.GetCustomerProfile().ConfigureAwait(false);
        }

        public async Task<ApiResponse<ProfileModel>> UpdateCustomerProfile(ProfileModel model)
        {
            if (model.birthday == null)
            {

                model.birthday = "1995-12-03";
            }
            return await _profileServices.UpdateCustomerProfile(model).ConfigureAwait(false);
        }
    }
}
