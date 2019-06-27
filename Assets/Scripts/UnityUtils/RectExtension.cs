using UnityEngine;


namespace EMUtils.UnityUtils
{
	public static class RectExtension
	{
		public static bool ContainsXZ(this Rect rect, Vector3 point)
		{
			return rect.Contains(new Vector2(point.x, point.z));
        }
	}
}
