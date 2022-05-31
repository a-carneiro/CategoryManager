using CategoryManager.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Text.Json.JsonElement;

namespace CategoryManager.Interface.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoryAsyn();
        Task CreateCategoryAsyn(ObjectEnumerator categoryDynamic);
        Task<Category> GetCategoryByNameAsync(string CategoryId);
        Task DeleteCategoryAsync(string categoryName);
    }
}