using System;

namespace CategoryManager.Domain.Exceptions
{
    public class CategoryDepthMaxWasReachedExceprion : Exception
    {
        public CategoryDepthMaxWasReachedExceprion() : base("The maximum depth value for the category has been reached")
        { }
    }
}