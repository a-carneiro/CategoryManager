using System;

namespace CategoryManager.Domain.Exceptions
{
    public class CategoryChildAlreadyLinkedToParentException : Exception
    {
        public CategoryChildAlreadyLinkedToParentException(string categoryName) : base($"The category '{categoryName}' alread linked to a parent")
        { }
    }
}
