using UnityEngine;


namespace EMUtils.UnityUtils
{
    public static class Vector3Extension
    {
        public static Vector3 Quantized(this Vector3 v, float q = 5.0f)
        {
            return new Vector3(v.x.Quantized(q), v.y.Quantized(q), v.z.Quantized(q));
        }
    }
}
