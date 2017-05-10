using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Framework.Extension
{
    public static class EnumerableExtension
    {
        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool desc)
        {
            return desc ? source.ThenByDescending(keySelector) : source.ThenBy(keySelector);
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool desc)
        {
            return desc ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
        }
    }
}
