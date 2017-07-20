using UnityEngine;


namespace EMUtils.UnityUtils
{
    public static class GameObjectExtension
    {
        public static void SetLayerIncChildren(this GameObject obj, int layer)
        {
            obj.layer = layer;

            foreach (Transform child in obj.transform)
            {
                SetLayerIncChildren(child.gameObject, layer);
            }
        }
    }
}
