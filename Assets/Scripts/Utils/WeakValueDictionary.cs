using System;
using System.Collections.Generic;
using System.Linq;

namespace EMUtils.Utils
{
    public sealed class WeakValueDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TValue : class
    {
        private const int MinRehashInterval = 500;

        private Dictionary<TKey, WeakReference<TValue>> dict = new Dictionary<TKey, WeakReference<TValue>>();
        private int version;
        private int cleanVersion;
        private int cleanGeneration;

        #region IDictionary<TKey,TValue> Members

        public ICollection<TKey> Keys
        {
            get
            {
                return dict.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var nullCount = 0;
                var result = new List<TValue>();
                foreach (var r in dict.Values)
                {
                    TValue val;
                    if (r.TryGetTarget(out val))
                    {
                        result.Add(val);
                    }
                    else
                    {
                        nullCount++;
                    }
                }
                if (nullCount > dict.Count / 4)
                {
                    Cleanup();
                }
                return result;
            }
        }

        public bool ContainsKey(TKey key)
        {
            MaybeCleanup();

            WeakReference<TValue> wr;
            return dict.TryGetValue(key, out wr);
        }

        public void Add(TKey key, TValue value)
        {
            this[key] = value;
        }

        public bool Remove(TKey key)
        {
            MaybeCleanup();

            return dict.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            MaybeCleanup();

            WeakReference<TValue> wr;
            if (dict.TryGetValue(key, out wr))
            {
                TValue target;
                if (wr.TryGetTarget(out target))
                {
                    value = target;
                    return true;
                }
            }

            value = null;
            return false;
        }

        public TValue this[TKey key]
        {
            get
            {
                throw new NotImplementedException("Don't use this[key], the semantics of throwing KeyNotFoundException when a weak reference is cleared would be strange.  Use TryGetValue instead.");
            }

            set
            {
                MaybeCleanup();
                dict[key] = new WeakReference<TValue>(value);
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>>

        /// <summary>
        /// Note that this is an overestimate (if a weak reference has been cleared recently,
        /// this call will count it as still present).
        /// </summary>
        public int Count
        {
            get
            {
                return dict.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this[item.Key] = item.Value;
        }

        public void Clear()
        {
            dict.Clear();
            version = 0;
            cleanVersion = 0;
            cleanGeneration = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>>

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var nullCount = 0;

            foreach (var kv in dict)
            {
                TValue target;
                if (kv.Value.TryGetTarget(out target))
                {
                    yield return new KeyValuePair<TKey, TValue>(kv.Key, target);
                }
                else
                {
                    nullCount++;
                }
            }

            if (nullCount > dict.Count / 4)
            {
                Cleanup();
            }
        }

        #endregion

        #region Cleanup

        private void MaybeCleanup()
        {
            version++;

            if (version - cleanVersion > MinRehashInterval + dict.Count)
            {
                int curGeneration = GC.CollectionCount(0);
                if (cleanGeneration != curGeneration)
                {
                    cleanGeneration = curGeneration;
                    Cleanup();
                    cleanVersion = version;
                }
            }
        }

        /// <summary>
        /// Remove any entries where the weak reference is no longer alive.
        /// </summary>
        /// <returns>
        /// true if any objects were removed.
        /// </returns>
        public bool Cleanup()
        {
            var oldCount = dict.Count;
            dict = dict.Where(kv => {
                TValue v;
                return kv.Value.TryGetTarget(out v);
            }).ToDictionary(kv => kv.Key, kv => kv.Value);
            return (oldCount > dict.Count);
        }

        #endregion
    }
}
