using CategoryManager.Interface.Repository;
using CategoryManager.Service.Tests.Fixture;
using Moq;
using System.Linq;

namespace CategoryManager.Service.Tests.Mock
{
    class CategoryServiceMock : ServiceMock<CategoryService, CategoryServiceFixture>
    {
        public Mock<ICategoryRepository> GetCategoryRepository() => AutoMocker.GetMock<ICategoryRepository>();

        public CategoryServiceMock(CategoryServiceFixture fixture) : base(fixture)
        {
            SetupRepository();
        }
        private void SetupRepository()
        {
            var serviceRepository = GetCategoryRepository();
            serviceRepository.Setup(x => x.GetAllCategoryAsync()).ReturnsAsync(Fixture.Categories);
            serviceRepository.Setup(x => x.GetCategoryByNameAsync(Fixture.CategorywithNoChildren.Name)).ReturnsAsync(Fixture.CategorywithNoChildren);
            serviceRepository.Setup(x => x.GetCategoryByNameAsync(Fixture.CategoryWithChildren.Name)).ReturnsAsync(Fixture.CategoryWithChildren);
            serviceRepository.Setup(x => x.GetCategoryByNameAsync(Fixture.CategoryWithChildren.Children.FirstOrDefault().Name)).ReturnsAsync(Fixture.CategoryWithChildren.Children.FirstOrDefault());
        }
    }
}
