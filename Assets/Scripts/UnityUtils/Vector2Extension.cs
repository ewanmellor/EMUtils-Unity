using UnityEngine;


namespace EMUtils.UnityUtils
{
    public static class Vector2Extension
    {
        /// <summary>
        /// Return a new point, derived from this point and the given rectangle,
        /// where the given point is projected out from the center of the rectangle
        /// to the nearest edge.
        /// </summary>
        public static Vector2 ProjectToRectEdge(this Vector2 pos,
                                                float rectWidth, float rectHeight,
                                                out float outAngleRad)
        {
            var center = new Vector2(rectWidth / 2f, rectHeight / 2f);
            var normalizedDir = (pos - center).normalized;
            var angleRad = Mathf.Atan2(normalizedDir.x, normalizedDir.y);
            var distHeight = Mathf.Abs(center.y / Mathf.Cos(angleRad));
            var distWidth = Mathf.Abs(center.x / Mathf.Cos(angleRad + MathfExtension.PI2));
            var shortestDist = Mathf.Min(distHeight, distWidth);
            outAngleRad = angleRad;
            return center + (normalizedDir * shortestDist);
        }
    }
}
