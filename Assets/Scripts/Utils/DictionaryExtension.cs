using System;
using System.Collections;
using System.Collections.Generic;


namespace EMUtils.Utils
{
    public static class DictionaryExtension
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue def = default)
        {
            return dict.TryGetValue(key, out TValue result) ? result : def;
        }


        public static TValue GetValueOrPut<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> generator)
        {
            if (dict.TryGetValue(key, out TValue result))
            {
                return result;
            }
            result = generator();
            dict[key] = result;
            return result;
        }


        public static void AddToList<TKey, TValue>(this IDictionary<TKey, List<TValue>> dict, TKey key, TValue val)
        {
            var l = dict.GetValueOrPut(key, () => new List<TValue>());
            l.Add(val);
        }


        /// <returns><c>true</c> if the given val was found and removed.</returns>
        public static bool RemoveFromList<TKey, TValue>(this IDictionary<TKey, List<TValue>> dict, TKey key, TValue val)
        {
            var l = dict.GetValueOrDefault(key);
            if (l == null)
            {
                return false;
            }
            var result = l.Remove(val);
            if (l.Count == 0)
            {
                dict.Remove(key);
            }
            return result;
        }


        public static bool ContentEquals(this IDictionary dict, IDictionary other)
        {
            if (dict == null || other == null || dict.Count != other.Count)
            {
                return false;
            }
            foreach (DictionaryEntry kv in dict)
            {
                object otherV = other[kv.Key];
                if (otherV == null || !kv.Value.Equals(otherV))
                {
                    return false;
                }
            }
            return true;
        }


        public static bool ContentEquals<TKey, TValue>(this IDictionary<TKey, TValue> dict, IDictionary<TKey, TValue> other)
        {
            if (dict == null || other == null || dict.Count != other.Count)
            {
                return false;
            }
            foreach (var kv in dict)
            {
                if (!other.TryGetValue(kv.Key, out TValue otherV))
                {
                    return false;
                }
                if (!kv.Value.Equals(otherV))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
