using UnityEngine;


namespace EMUtils.UnityUtils
{
    public static class MathfExtension
    {
        public const float OneOverSqrt2 = 0.70710676908493f;

        public static float Quantized(this float i, float q = 5.0f)
        {
            return Mathf.Round(i / q) * q;
        }
    }
}
