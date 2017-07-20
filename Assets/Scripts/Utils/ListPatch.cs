using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace EMUtils.Utils
{
    public sealed class ListPatch<T> where T : class
    {
        readonly List<T> List;
        readonly Dictionary<int, T> OldValues;
        readonly Dictionary<int, T> NewValues;


        public ListPatch(List<T> list, Dictionary<int, T> newValues)
        {
            Debug.Assert(list != null);
            Debug.Assert(newValues != null);
            foreach (var kv in newValues)
            {
                Debug.Assert(kv.Key == -1 || kv.Key < list.Count);
            }

            List = list;
            NewValues = newValues;
            OldValues = newValues.ToDictionary(x => x.Key, x => x.Key == -1 ? null : list[x.Key]);               
        }


        public void ApplyPatch()
        {
            SetValues(NewValues);
        }


        public void UndoPatch()
        {
            SetValues(OldValues);
        }


        void SetValues(Dictionary<int, T> vals)
        {
            foreach (var kv in vals)
            {
                var idx = kv.Key;
                var val = kv.Value;
                if (idx == -1)
                {
                    if (val == null)
                    {
                        List.RemoveLast();
                    }
                    else
                    {
                        List.Add(val);
                    }
                }
                else if (val == null)
                {
                    Debug.Assert(idx == List.Count - 1);
                    List.RemoveLast();
                }
                else
                {
                    List[idx] = val;
                }
            }
        }
    }
}
