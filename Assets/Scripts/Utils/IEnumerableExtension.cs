using System;
using System.Collections.Generic;
using System.Linq;


namespace EMUtils.Utils
{
    public static class IEnumerableExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, TResult> selector
            ) where TResult : class
        {
            return source.Select(selector).WhereNotNull();
        }

        public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(
            this IEnumerable<TSource> source, Func<TSource, int, TResult> selector
            ) where TResult : class
        {
            return source.Select(selector).WhereNotNull();
        }

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source) where T : class
        {
            return source.Where(x => x != null);
        }
    }
}
