using System.Collections.Generic;


namespace EMUtils.Utils
{
    public static class IListExtension
    {
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return list == null || list.Count == 0;
        }

        /// <returns>null if this is non-empty, this list otherwise.</returns>
        public static List<T> NullIfEmpty<T>(this List<T> list)
        {
            return list.IsNullOrEmpty() ? null : list;
        }

        public static void RemoveLast<T>(this IList<T> list)
        {
            list.RemoveAt(list.Count - 1);
        }
    }
}
