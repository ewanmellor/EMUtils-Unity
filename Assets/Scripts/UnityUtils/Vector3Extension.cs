using UnityEngine;


namespace EMUtils.UnityUtils
{
    public static class Vector3Extension
    {
        public static Vector2 WithoutZ(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static Vector3 Quantized(this Vector3 v, float q = 5.0f)
        {
            return new Vector3(v.x.Quantized(q), v.y.Quantized(q), v.z.Quantized(q));
        }


        public static float SquaredDistance(Vector3 a, Vector3 b)
        {
            return (b - a).sqrMagnitude;
        }
    }
}
