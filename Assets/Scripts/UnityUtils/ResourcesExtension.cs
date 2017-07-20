﻿using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace EMUtils.UnityUtils
{
    public static class ResourcesExtension
    {
        #if UNITY_EDITOR
        public static IEnumerable<T> FindNonPrefabsOfType<T>() where T : Component
        {
            var objs = UnityEngine.Resources.FindObjectsOfTypeAll<T>();
            foreach (var obj in objs)
            {
                var go = obj.gameObject;
                var pt = PrefabUtility.GetPrefabType(go);
                if (pt != PrefabType.Prefab)
                {
                    yield return obj;
                }
            }
        }
        #endif
    }
}
