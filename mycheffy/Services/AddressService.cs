using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using mycheffy.Models.Address;
using mycheffy.Models.Shared.Enums;
using mycheffy.Services.Interfaces;
using mycheffy.Services.Middleware;
using Refit;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mycheffy.Services
{
    public class AddressService : IAddressServices
    {
        private readonly HttpClient _httpClient;
        private readonly IAddressServices _addressApi;

        private async Task<string> GetToken()
        {
            //var token = SecureStorage.GetAsync(SecureStorageKey.OAuthToken.ToString());
            var token = Preferences.Get(PreferencesKey.AccessToken.ToString(), string.Empty);
            Console.WriteLine("Address Services GetToken: " + token);
            return token;
        }

        public AddressService()
        {
            var authenticatedHandler = new AuthenticatedHandler(GetToken) { InnerHandler = new HttpClientHandler() };
            _httpClient = new HttpClient(authenticatedHandler) { BaseAddress = new Uri(NetworkService.baseUrl) };

            _addressApi = RestService.For<IAddressServices>(_httpClient);
        }

        public async Task<IEnumerable<AddressModel>> GetAllUserAddress()
        {
            return await _addressApi.GetAllUserAddress().ConfigureAwait(false);
        }

        public async Task<AddressModel> DeleteUserAddress(int id)
        {
            return (AddressModel)await _addressApi.DeleteUserAddress(id).ConfigureAwait(false);
        }

        public async Task<ApiResponse<AddressModel>> AddNewUserAddress(string type, string name, string complete_address, string full_google_address, double latitude, double longitude, string phone_number, int is_primary)
        {
            return await _addressApi.AddNewUserAddress(type,
                                                       name,
                                                       complete_address,
                                                       full_google_address,
                                                       latitude,
                                                       longitude,
                                                       phone_number,
                                                       is_primary)
                                    .ConfigureAwait(false);
        }

        public async Task<ApiResponse<AddressModel>> UpdateUserAddress(int id, string type, string name, string complete_address, string full_google_address, double latitude, double longitude, string phone_number, int is_primary)
        {
            return await _addressApi.UpdateUserAddress(id,
                                                       type,
                                                       name,
                                                       complete_address,
                                                       full_google_address,
                                                       latitude,
                                                       longitude,
                                                       phone_number,
                                                       is_primary)
                                    .ConfigureAwait(false);
        }
    }
}
