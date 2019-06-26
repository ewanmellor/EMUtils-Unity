using UnityEngine;


namespace EMUtils.UnityUtils
{
    public static class RectTransformExtension
    {
        public static bool ContainsScreenPoint(this RectTransform rectTransform, Vector3 point)
        {
            var localPosition = rectTransform.InverseTransformPoint(point);
            return rectTransform.rect.Contains(localPosition);
        }
    }
}
