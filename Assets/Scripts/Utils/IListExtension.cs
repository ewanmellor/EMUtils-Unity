using System.Collections.Generic;


namespace EMUtils.Utils
{
    public static class IListExtension
    {
        public static void RemoveLast<T>(this IList<T> list)
        {
            list.RemoveAt(list.Count - 1);
        }
    }
}
