using CategoryManager.Domain.Entity;
using CategoryManager.Interface.Repository;
using CategoryManager.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoryManager.Repository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ILogger<CategoryRepository> _logger;
        private readonly CategoryManagerContext _categoryManagerContext;

        public CategoryRepository(ILogger<CategoryRepository> logger, CategoryManagerContext categoryManagerContext)
        {
            _logger = logger;
            _categoryManagerContext = categoryManagerContext;
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            var result = await _categoryManagerContext.Categories
                .Include(x => x.Children)
                .ToListAsync();

            if (!result.Any())
                return result;

            return result.GroupBy(x => x.IsChild)
                .FirstOrDefault(x => x.Key == false)
                .ToList();
        }

        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            return (await _categoryManagerContext.Categories
                .Include(x => x.Children).ToListAsync())
                .FirstOrDefault(x => x.Name.Equals(categoryName));
        }

        public async Task CreateCategoryAsync(Category category)
        {
            _categoryManagerContext.Categories.UpdateRange(category);
            await _categoryManagerContext.SaveChangesAsync();
        }

        public async Task CreateCategoriesAsync(IEnumerable<Category> category)
        {
            try
            {
                _categoryManagerContext.UpdateRange(category);
                await _categoryManagerContext.SaveChangesAsync();

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task DeleteCategoryAsync(Category category)
        {
            try
            {
                _categoryManagerContext.Categories.Remove(category);
                await _categoryManagerContext.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
