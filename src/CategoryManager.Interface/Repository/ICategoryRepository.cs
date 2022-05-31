using CategoryManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryManager.Interface.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoryAsync();
        Task<Category> GetCategoryByNameAsync(string categoryId);
        Task CreateCategoryAsync(Category category);
        Task CreateCategoriesAsync(IEnumerable<Category> category);
        Task DeleteCategoryAsync(Category category);
    }
}
