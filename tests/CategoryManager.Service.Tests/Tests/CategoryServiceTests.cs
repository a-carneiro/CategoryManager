using CategoryManager.Domain.Entity;
using CategoryManager.Domain.Exceptions;
using CategoryManager.Service.Tests.Fixture;
using CategoryManager.Service.Tests.Mock;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CategoryManager.Service.Tests.Tests
{
    [Trait("Category", "Service")]
    public class CategoryServiceTests : IClassFixture<CategoryServiceFixture>
    {
        private readonly CategoryServiceFixture _fixture;
        private readonly CategoryServiceMock _mock;

        public CategoryServiceTests(CategoryServiceFixture fixture)
        {
            _fixture = fixture;
            _mock = new CategoryServiceMock(_fixture);
        }

        [Fact]
        public async Task GetAllCategoryAsyn_CategoryExists_ShouldReturnCategories()
        {
            //Arrange
            var modelApplication = _mock.GetService();
            //Act
            var result = await modelApplication.GetAllCategoryAsyn();
            //Assert
            result.Should().NotBeNullOrEmpty();
            result.FirstOrDefault().Name.Should().Be(_fixture.Categories.FirstOrDefault().Name);
        }

        [Fact]
        public async Task GetAllCategoryAsyn_CategoryNotExists_ShouldReturnNull()
        {
            //Arrange
            var repository = _mock.GetCategoryRepository();
            var modelApplication = _mock.GetService();

            repository.Setup(x => x.GetAllCategoryAsync()).ReturnsAsync((List<Category>)null);
            //Act
            var result = await modelApplication.GetAllCategoryAsyn();
            //Assert
            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetCategoryByNameAsync_CategoryFound_ShouldReturnCategory()
        {
            //Arrange
            var modelApplication = _mock.GetService();
            //Act
            var result = await modelApplication.GetCategoryByNameAsync(_fixture.CategorywithNoChildren.Name);
            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(_fixture.CategorywithNoChildren.Name);
        }

        [Fact]
        public async Task GetCategoryByNameAsync_CategoryWasNotFound_ShouldReturnNull()
        {
            //Arrange
            var repository = _mock.GetCategoryRepository();
            var modelApplication = _mock.GetService();

            repository.Setup(x => x.GetCategoryByNameAsync(It.IsAny<string>())).ReturnsAsync((Category)null);
            //Act
            var result = await modelApplication.GetCategoryByNameAsync(It.IsAny<string>());
            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateCategoryAsyn_CategoryOk_ShouldCreate()
        {
            //Arrange
            var repository = _mock.GetCategoryRepository();
            var modelApplication = _mock.GetService();

            using JsonDocument doc = JsonDocument.Parse(_fixture.BasicCreateCategoryRequest);
            JsonElement root = doc.RootElement;

            //Act
            await modelApplication.CreateCategoryAsyn(root.EnumerateObject());
            //Assert
            repository.Verify(x => x.CreateCategoriesAsync(It.IsAny<IEnumerable<Category>>()), Times.Once);
        }

        [Fact]
        public async Task CreateCategoryAsyn_CategoryChildAlreadyLinkedToParentException_ShouldException()
        {
            //Arrange
            var modelApplication = _mock.GetService();

            using JsonDocument doc = JsonDocument.Parse(_fixture.CreateCategoryChildLinked);
            JsonElement root = doc.RootElement;

            //Act and Assert
            await Assert.ThrowsAsync<CategoryChildAlreadyLinkedToParentException>(() => modelApplication.CreateCategoryAsyn(root.EnumerateObject()));
        }

        [Fact]
        public async Task CreateCategoryAsyn_CategoryDepthMaxWasReachedExceprion_ShouldException()
        {
            //Arrange
            var modelApplication = _mock.GetService();

            using JsonDocument doc = JsonDocument.Parse(_fixture.CreateCategoryMaxDepthValue);
            JsonElement root = doc.RootElement;

            //Act and Assert
            await Assert.ThrowsAsync<CategoryDepthMaxWasReachedExceprion>(() => modelApplication.CreateCategoryAsyn(root.EnumerateObject()));
        }

        [Fact]
        public async Task DeleteCategoryAsync_CategoryOk_ShouldCreate()
        {
            //Arrange
            var repository = _mock.GetCategoryRepository();
            var modelApplication = _mock.GetService();
            //Act
            await modelApplication.DeleteCategoryAsync(_fixture.CategoryWithChildren.Name);
            //Assert
            repository.Verify(x => x.DeleteCategoryAsync(It.IsAny<Category>()), Times.AtLeast(2));
        }

        [Fact]
        public async Task DeleteCategoryAsync_CategoryDepthMaxWasReachedExceprion_ShouldException()
        {
            //Arrange
            var repository = _mock.GetCategoryRepository();
            var modelApplication = _mock.GetService();

            repository.Setup(x => x.GetCategoryByNameAsync(_fixture.CategoryWithChildren.Name)).ReturnsAsync((Category)null);

            //Act and Assert
            await Assert.ThrowsAsync<CategoryNotFoundExceprion>(() => modelApplication.DeleteCategoryAsync(_fixture.CategoryWithChildren.Name));
        }
    }
}