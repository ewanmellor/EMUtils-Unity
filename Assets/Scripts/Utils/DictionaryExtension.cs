using System.Collections.Generic;


namespace EMUtils.Utils
{
    public static class DictionaryExtension
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue def = default(TValue))
        {
            TValue result;
            return dict.TryGetValue(key, out result) ? result : def;
        }
    }
}
