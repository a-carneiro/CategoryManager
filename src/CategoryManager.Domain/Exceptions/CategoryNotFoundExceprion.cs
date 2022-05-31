using System;

namespace CategoryManager.Domain.Exceptions
{
    public class CategoryNotFoundExceprion : Exception
    {
        public CategoryNotFoundExceprion(string categoryName) : base($"The category '{categoryName}' was not found")
        { }
    }
}
