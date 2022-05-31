using System.ComponentModel.DataAnnotations;

namespace CategoryManager.Domain.Entity
{
    public class CategoryChild
    {
        public int Id { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string? ChildId { get; set; }
        public Category? Child { get; set; }
    }
}
