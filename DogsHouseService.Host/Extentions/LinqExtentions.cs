using DogsHouseService.Host.Data.Entities;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DogsHouseService.Host.Extentions
{
    public static class LinqExtentions
    {
        public static IEnumerable<DogEntity> SortBy(this IEnumerable<DogEntity> collection, string attribute, string order)
        {
            bool isAscending = string.Equals(order, "Asc", StringComparison.OrdinalIgnoreCase);
            return attribute switch
            {
                "name" => collection.OrderBy(x => x.Name, isAscending),
                "color" => collection.OrderBy(x => x.Color, isAscending), 
                "tailLength" => collection.OrderBy(x => x.TailLength, isAscending),
                "weight" => collection.OrderBy(x => x.Weight, isAscending),
                _ => collection
            };
        }

        private static IEnumerable<T> OrderBy<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> selector, bool isAscending)
        {
            return isAscending? collection.OrderBy(selector) : collection.OrderByDescending(selector);
        }
    }
}
