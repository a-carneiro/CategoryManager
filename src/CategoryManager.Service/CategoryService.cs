using CategoryManager.Domain.Entity;
using CategoryManager.Domain.Exceptions;
using CategoryManager.Interface.Repository;
using CategoryManager.Interface.Service;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Text.Json.JsonElement;

namespace CategoryManager.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<Category>> GetAllCategoryAsyn()
        {
            return await _categoryRepository.GetAllCategoryAsync();
        }
        public async Task<Category> GetCategoryByNameAsync(string CategoryId)
        {
            return await _categoryRepository.GetCategoryByNameAsync(CategoryId);
        }
        public async Task CreateCategoryAsyn(ObjectEnumerator categoryDynamic)
        {
            var categoryList = new List<Category>();
            var gouped = categoryDynamic.GroupBy(x => x.Name);

            await _categoryRepository.CreateCategoriesAsync(await ProcessCategoryAsync(gouped));
        }
        public async Task DeleteCategoryAsync(string categoryName)
        {
            var category = await GetCategoryByNameAsync(categoryName);

            if (category == null)
                throw new CategoryNotFoundExceprion(categoryName);

            await CascateDelete(category);

            category.Children = null;
            await _categoryRepository.DeleteCategoryAsync(category);
        }
        private async Task CascateDelete(Category category)
        {
            foreach (var child in category.Children)
            {
                if (child.Children.Any())
                {
                    await CascateDelete(child);
                }
                await _categoryRepository.DeleteCategoryAsync(child);
            }
        }
        private async Task<List<Category>> ProcessCategoryAsync(IEnumerable<IGrouping<string, JsonProperty>> categoriesGroup)
        {
            var categoryBag = new ConcurrentBag<Category>();

            var tasks = categoriesGroup.Select(async categories =>
            {
                int level = 0;
                var dbCategory = await GetCategoryByNameAsync(categories.Key);

                if (dbCategory == null)
                    dbCategory = new Category
                    {
                        Name = categories.Key,
                        IsChild = false,
                        DeepLevel = level
                    };
                else if (dbCategory.DeepLevel >= 10)
                    throw new CategoryDepthMaxWasReachedExceprion();

                level = dbCategory.DeepLevel;
                level++;

                var CategoryChildren = new List<Category>();
                foreach (var category in categories)
                {
                    string categoryName = category.Value.ToString();

                    var categoryChild = await GetCategoryByNameAsync(categoryName);

                    if (categoryChild == null || categoryName.Equals(dbCategory.Name))
                    {
                        CategoryChildren.Add(new Category
                        {
                            Name = categoryName,
                            IsChild = true,
                            DeepLevel = level
                        });
                    }
                    else
                        throw new CategoryChildAlreadyLinkedToParentException(categoryName);
                }

                dbCategory.AddRangeChild(CategoryChildren);

                categoryBag.Add(dbCategory);
            });

            await Task.WhenAll(tasks);

            return categoryBag.ToList();
        }
    }
}