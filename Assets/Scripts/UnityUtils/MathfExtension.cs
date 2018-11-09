using System;

using UnityEngine;


namespace EMUtils.UnityUtils
{
    public static class MathfExtension
    {
        public const float OneOverSqrt2 = 0.70710676908493f;
        public const float PI2 = Mathf.PI / 2;


        /// <summary>
        /// Find a local minimum for the given function.
        /// </summary>
        /// <returns>The minimum.</returns>
        /// <param name="minX">Search range minimum.</param>
        /// <param name="maxX">Search range maximum.</param>
        /// <param name="f">The function to minimize.</param>
        /// <param name="epsilon">Epsilon for the search.</param>
        public static float LocalMinimum(float minX, float maxX, Func<float, float> f, float epsilon = 1e-10f)
        {
            var m = minX;
            var n = maxX;
            var k = minX;

            while ((n - m) > epsilon)
            {
                k = (n + m) / 2.0f;
                if (f(k - epsilon) < f(k + epsilon))
                {
                    n = k;
                }
                else
                {
                    m = k;
                }
            }
            return k;
        }


        public static float Quantized(this float i, float q = 5.0f)
        {
            return Mathf.Round(i / q) * q;
        }
    }
}
