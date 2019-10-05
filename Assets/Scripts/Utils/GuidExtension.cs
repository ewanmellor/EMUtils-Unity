using System;

namespace EMUtils.Utils
{
    public static class GuidExtension
    {
        public static Guid FromStringBase64url(string s)
        {
            var arr = ArrayExtension.FromStringBase64url(s);
            SwapBytes(arr);
            return new Guid(arr);
        }

        public static byte[] ToByteArrayCorrectedOrder(this Guid guid)
        {
            var arr = guid.ToByteArray();
            SwapBytes(arr);
            return arr;
        }

        public static string ToStringBase64url(this Guid guid)
        {
            return guid.ToByteArrayCorrectedOrder().ToStringBase64url();
        }

        public static string ToStringTiny(this Guid guid)
        {
            return guid.ToStringBase64url().Substring(0, 4);
        }

        /// <summary>
        /// Per the Guid.ToByteArray docs, the byte array used by
        /// System.Guid is mixed up, compared with the written
        /// form of the Guid (and everyone else's usage).
        ///
        /// This method inverts the first group of four bytes, and
        /// the second two groups of two bytes.
        /// </summary>
        private static void SwapBytes(this byte[] arr)
        {
            var tmp = arr[0];
            arr[0] = arr[3];
            arr[3] = tmp;
            tmp = arr[1];
            arr[1] = arr[2];
            arr[2] = tmp;
            tmp = arr[4];
            arr[4] = arr[5];
            arr[5] = tmp;
            tmp = arr[6];
            arr[6] = arr[7];
            arr[7] = tmp;
        }
    }
}
