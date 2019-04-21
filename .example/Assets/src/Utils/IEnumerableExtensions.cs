using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Example.Utils
{
    public static class IEnumerableExtensions
    {
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            var count = enumerable.Count();
            return count > 0 ? enumerable.ElementAt(Random.Range(0, count)) : default;
        }
    }
}
