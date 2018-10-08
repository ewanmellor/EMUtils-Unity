using System;
using System.Collections.Generic;
using System.Linq;


namespace EMUtils.Utils
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> AsNotNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        /// <returns>The result of ToList() if that is non-empty, or null
        /// otherwise.</returns>
        public static List<T> ToListOrNullIfEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToList().NullIfEmpty();
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
