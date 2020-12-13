using System;
using System.Threading.Tasks;
using mycheffy.Models.Register;
using Refit;

namespace mycheffy.Services.Interfaces
{
    [Headers("Accept: application/json")]
    internal interface IRegisterServices
    {
        [Post("/register")]
        Task<ApiResponse<RegisterModel>> Register(string first_name, //firstname of user
                                                string last_name, //lastname of user
                                                string email, //email of user
                                                string password, //password of user
                                                string password_confirmation, //value should be same with password
                                                string type, //value is email or sns
                                                string sns, //null or facebook, google
                                                string sns_id, //user if from sns
                                                string photo, //photo from sns
                                                int is_vendor); //user type, 0 = customer, 1 = vendor
    }
}
