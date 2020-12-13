using System;
using System.Threading.Tasks;
using mycheffy.Models.Login;
using Refit;

namespace mycheffy.Services.Interfaces
{
    [Headers("Accept: application/json")]
    internal interface ILoginServices
    {
        [Multipart]
        [Post("/oauth/token")]
        Task<ApiResponse<LoginModel>> SignIn(string grant_type, //password
                            string client_id, //from API
                            string client_secret, //from API
                            string username, //username of user
                            string password, //password of user
                            string scope,
                            string type, //email or sns
                            string provider, //null or facebook,google
                            string name, //nullable enter name from SNS if login without registering
                            string photo); // enter name from SNS if login without registering
    }
}
