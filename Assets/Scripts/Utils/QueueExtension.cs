using System.Collections.Generic;


namespace EMUtils.Utils
{
    public static class QueueExtension
    {
#if !NETCOREAPP2_0 && !NETCOREAPP2_1
        public static bool TryDequeue<T>(this Queue<T> queue, out T result)
        {
            if (queue.Count == 0)
            {
                result = default;
                return false;
            }
            else
            {
                result = queue.Dequeue();
                return true;
            }
        }
#endif
    }
}
