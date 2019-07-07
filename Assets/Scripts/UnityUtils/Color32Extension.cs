using UnityEngine;


namespace EMUtils.UnityUtils
{
    public static class Color32Extension
    {
        public static readonly Color32 Transparent = new Color32(0, 0, 0, 0);
        public static readonly Color32 White = new Color32(255, 255, 255, 255);


        public static Color32 AlphaOverlay(this Color32 back, Color32 front)
        {
            if (front.a == 0)
            {
                return back;
            }
            if (front.a == 255 || back.a == 0)
            {
                return front;
            }
            if (back.a == 255)
            {
                var frontA = front.a + 1;
                var invFrontA = 256 - front.a;
                return new Color32(
                    (byte)((back.r * invFrontA + front.r * frontA) >> 8),
                    (byte)((back.g * invFrontA + front.g * frontA) >> 8),
                    (byte)((back.b * invFrontA + front.b * frontA) >> 8),
                    (byte)255);
            }
            else
            {
                var frontA = front.a / 255f;
                var invFrontA = 1f - frontA;
                var backA = back.a / 255f;
                var resultA = frontA + backA * invFrontA;

                var resultR = (front.r / 255f) * frontA + (back.r / 255f * backA) * invFrontA;
                var resultG = (front.g / 255f) * frontA + (back.g / 255f * backA) * invFrontA;
                var resultB = (front.b / 255f) * frontA + (back.b / 255f * backA) * invFrontA;

                return new Color32(
                    (byte)Mathf.Round(255f * resultR / resultA),
                    (byte)Mathf.Round(255f * resultG / resultA),
                    (byte)Mathf.Round(255f * resultB / resultA),
                    (byte)Mathf.Round(255f * resultA));
            }
        }
    }
}
