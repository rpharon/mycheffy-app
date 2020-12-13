using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mycheffy.Models.Address;
using Refit;

namespace mycheffy.Services.Interfaces
{
    [Headers("Accept: application/json")]
    internal interface IAddressServices
    {
        [Get("/profile/addresses")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<AddressModel>> GetAllUserAddress();

        [Delete("/profile/addresses/{id}")]
        [Headers("Authorization: Bearer")]
        Task<AddressModel> DeleteUserAddress(int id);

        [Post("/profile/addresses")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<AddressModel>> AddNewUserAddress(string type,
                                                        string name,
                                                        string complete_address,
                                                        string full_google_address,
                                                        double latitude,
                                                        double longitude,
                                                        string phone_number,
                                                        int is_primary);

        [Put("/profile/addresses/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<AddressModel>> UpdateUserAddress(int id,
                                                        string type,
                                                        string name,
                                                        string complete_address,
                                                        string full_google_address,
                                                        double latitude,
                                                        double longitude,
                                                        string phone_number,
                                                        int is_primary);
    }
}
