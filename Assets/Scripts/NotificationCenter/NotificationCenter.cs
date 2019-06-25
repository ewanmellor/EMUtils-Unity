using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using EMUtils.Utils;


namespace EMUtils.NotificationCenter
{
    public sealed class NotificationCenter
    {
        public delegate void NotificationDelegate(IDictionary senderInfo);


        static NotificationCenter _Instance;

        public static NotificationCenter Instance
        {
            get
            {
                EMAssert.OnMainThread();

                if (_Instance == null)
                {
                    _Instance = new NotificationCenter();
                }
                return _Instance;
            }
        }


        readonly Dictionary<string, List<NotificationDelegate>> observers = new Dictionary<string, List<NotificationDelegate>>();


        public void AddObserver(string name, NotificationDelegate del)
        {
            observers.AddToList(name, del);
        }


        public void RemoveObserver(string name, NotificationDelegate del)
        {
            observers.RemoveFromList(name, del);
        }


        public void Notify(string name, IDictionary senderInfo = null)
        {
            EMAssert.OnMainThread();

            var obs = observers.GetValueOrDefault(name);
            if (obs == null)
            {
                return;
            }
            foreach (var ob in obs)
            {
                try
                {
                    ob(senderInfo);
                }
                catch (Exception err)
                {
                    Debug.LogErrorFormat("Squelching error in NotificationCenter.Notify: {0}", err);
                }
            }
        }
    }
}
