using App.Framework.MapperUtils;
using System;
using System.Collections.Generic;
using System.Data;
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

        public static IEnumerable<T> Select<T>(this IDataReader reader)
        {
            while (reader.Read())
            {
                yield return Mapper.Map<T>(reader);
            }
        }

        public static IEnumerable<T> Select<T>(this IDataReader reader,
                                       Func<IDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }

        public static bool HasRows(this IDataReader reader)
        {
            while (reader.Read())
            {
                return true;
            }

            return false;
        }
    }
}
