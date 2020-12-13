using mycheffy.Models.Category;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mycheffy.Services.Interfaces
{
    [Headers("Accept: application/json")]
    internal interface ICategoryServices
    {
        [Get("/categories")]
        Task<IEnumerable<CategoryModel>> GetAll();
    }
}