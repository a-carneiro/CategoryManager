using CategoryManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Text.Json.JsonElement;

namespace CategoryManager.Service.Tests.Fixture
{
    public class CategoryServiceFixture
    {
        public Category CategoryWithChildren { get; set; }
        public Category CategorywithNoChildren { get; set; }
        public List<Category> Categories { get; set; }
        public string BasicCreateCategoryRequest { get; set; }
        public string CreateCategoryChildLinked { get; set; }
        public string CreateCategoryMaxDepthValue { get; set; }
        public CategoryServiceFixture()
        {
            SetUpData();
        }

        private void SetUpData()
        {
            var category = new Category
            {
                Id = 1,
                Name = "test",
                DeepLevel = 0,
                IsChild = false,
            };

            CategoryWithChildren = category;
            CategoryWithChildren.AddRangeChild(new List<Category>
            {
                new Category
                 {
                     Id = 2,
                     Name = "test2",
                     DeepLevel = 10,
                     IsChild = true,
                 }
            });

            CategorywithNoChildren = category;

            Categories = new List<Category>
            {
                category
            };

            BasicCreateCategoryRequest = "{\"Postcards\": \"5 inch x 6 inch Postcards\",\"Postcards\": \"6 inch x 7 inch Postcards\"}";
            CreateCategoryChildLinked = "{\"Postcards\": \"test\"}";
            CreateCategoryMaxDepthValue = "{\"test2\": \"test2\"}";


        }

    }
}
