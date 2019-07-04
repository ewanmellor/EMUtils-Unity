using UnityEngine;


namespace EMUtils.UnityUtils
{
    public static class Vector3Extension
    {
        const float kEpsilonSquared = Vector3.kEpsilon * Vector3.kEpsilon;


        public static Vector2 WithoutZ(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static Vector3 Quantized(this Vector3 v, float q = 5.0f)
        {
            return new Vector3(v.x.Quantized(q), v.y.Quantized(q), v.z.Quantized(q));
        }


        /// <returns>Vector3.Cross(a, b).normalized, except without allocating the temporary.</returns>
        public static Vector3 CrossNormalized(Vector3 a, Vector3 b)
        {
            float cx = a.y * b.z - a.z * b.y;
            float cy = a.z * b.x - a.x * b.z;
            float cz = a.x * b.y - a.y * b.x;

            float sqrMag = cx * cx + cy * cy + cz * cz;
            if (sqrMag <= kEpsilonSquared)
            {
                return Vector3.zero;
            }

            float mag = Mathf.Sqrt(sqrMag);
            return new Vector3(
                cx / mag,
                cy / mag,
                cz / mag
                );
        }


        /// <returns>(b - a).normalized, except without allocating the temporary.</returns>
        public static Vector3 DifferenceNormalized(Vector3 a, Vector3 b)
        {
            float dx = b.x - a.x;
            float dy = b.y - a.y;
            float dz = b.z - a.z;

            float sqrMag = dx * dx + dy * dy + dz * dz;
            if (sqrMag <= kEpsilonSquared)
            {
                return Vector3.zero;
            }

            float mag = Mathf.Sqrt(sqrMag);
            return new Vector3(
                dx / mag,
                dy / mag,
                dz / mag
                );
        }


        /// <returns>(b - a).sqrMagnitude, except without allocating the temporary.</returns>
        public static float SquaredDistance(Vector3 a, Vector3 b)
        {
            float dx = b.x - a.x;
            float dy = b.y - a.y;
            float dz = b.z - a.z;
            return dx * dx + dy * dy + dz * dz;
        }


        /// <returns>Vector3.Cross(a, b).sqrMagnitude, except without allocating the temporary.</returns>
        public static float CrossSquaredMagnitude(Vector3 a, Vector3 b)
        {
            float cx = a.y * b.z - a.z * b.y;
            float cy = a.z * b.x - a.x * b.z;
            float cz = a.x * b.y - a.y * b.x;
            return cx * cx + cy * cy + cz * cz;
        }


        /// <returns>(a + b).sqrMagnitude, except without allocating the temporary.</returns>
        public static float SumSquaredMagnitude(Vector3 a, Vector3 b)
        {
            float dx = b.x + a.x;
            float dy = b.y + a.y;
            float dz = b.z + a.z;
            return dx * dx + dy * dy + dz * dz;
        }
    }
}
