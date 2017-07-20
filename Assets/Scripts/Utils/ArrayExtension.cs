using System;
using System.Linq;


namespace EMUtils.Utils
{
    public static class ArrayExtension
    {
        public static T[] Flatten<T>(this T[][] arr)
        {
            int resultLen = arr.Sum(x => x.Length);
            T[] result = new T[resultLen];
            int resultIdx = 0;
            foreach (var a in arr)
            {
                int aLen = a.Length;
                Array.Copy(a, 0, result, resultIdx, aLen);
                resultIdx += aLen;
            }
            return result;
        }

        public static T[] TrimmedToLength<T>(this T[] arr, int n)
        {
            if (arr.Length == n)
            {
                return arr;
            }
            T[] result = new T[n];
            Array.Copy(arr, result, n);
            return result;
        }
    }
}
