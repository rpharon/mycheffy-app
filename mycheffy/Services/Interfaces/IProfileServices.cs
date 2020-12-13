using mycheffy.Models.Profile;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mycheffy.Services.Interfaces
{
    [Headers("Accept: application/json")]
    internal interface IProfileServices
    {
        [Get("/profile")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<ProfileModel>> GetCustomerProfile();

        [Put("/profile")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<ProfileModel>> UpdateCustomerProfile(ProfileModel model);
    }
}
