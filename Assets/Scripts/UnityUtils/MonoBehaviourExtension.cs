using System;
using System.Collections;
using UnityEngine;


namespace EMUtils.UnityUtils
{
    public static class MonoBehaviourExtension
    {
        public static void InvokeNextFrame(this MonoBehaviour mb, Action action)
        {
            mb.StartCoroutine(_InvokeNextFrame(action));    
        }

        private static IEnumerator _InvokeNextFrame(Action action)
        {
            yield return null;
            action();
        }
    }
}
