using CategoryManager.Domain.Entity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoryManager.Web.Mapping
{
    public static class CategoryMapp
    {
        public static Object ToResponseModel(this IEnumerable<Category> categories)
        {
            if (categories is null)
                return new Object();

            var primeNumbers = new ConcurrentBag<dynamic>();

            Parallel.ForEach(categories, field =>
            {
                dynamic exo = new ExpandoObject();
                if (field.Children is null)
                    ((IDictionary<String, Object>)exo).Add(field.Name, new Object());

                else
                    ((IDictionary<String, Object>)exo).Add(field.Name, field.Children.ToResponseModel());

                primeNumbers.Add(exo);
            });

            return primeNumbers.ToList();
        }
    }
}
