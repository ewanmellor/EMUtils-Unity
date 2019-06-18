using UnityEngine;


namespace EMUtils.UnityUtils
{
    public static class GameObjectExtension
    {
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() ?? obj.AddComponent<T>();
        }

        public static GameObject NthChildWithTag(this GameObject obj, int n, string tag)
        {
            int idx = 0;
            foreach (Transform child in obj.transform)
            {
                if (child.CompareTag(tag))
                {
                    if (idx == n)
                    {
                        return child.gameObject;
                    }
                    idx += 1;
                }
            }
            return null;
        }

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
