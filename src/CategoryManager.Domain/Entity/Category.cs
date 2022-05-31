using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CategoryManager.Domain.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChild { get; set; }
        public int DeepLevel { get; set; }
        public IEnumerable<Category> Children { get; set; }

        public void AddRangeChild(List<Category> child)
        {
            if (this.Children == null)
                this.Children = new List<Category>();

            var cildrenList = this.Children.ToList();
            cildrenList.AddRange(child);
            this.Children = cildrenList;
        }

        public void SetChildrenToNull() => this.Children = null;
    }
}