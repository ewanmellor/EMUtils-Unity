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


        public static byte[] FromStringBase64url(string s)
        {
            var b = s.Replace("-", "+").Replace("_", "/");
            switch (b.Length % 4)
            {
                case 3:
                    b += "=";
                    break;
                case 2:
                    b += "==";
                    break;
                case 1:
                    b += "===";
                    break;
            }
            return Convert.FromBase64String(b);
        }

        public static string ToStringBase64url(this byte[] arr)
        {
            return Convert.ToBase64String(arr).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
    }
}
