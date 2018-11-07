using System;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

using EMUtils.Utils;


namespace EMUtils.Dispatcher
{
    [DisallowMultipleComponent]
    public class Dispatcher : MonoBehaviour
    {
        public static Dispatcher Instance;

        /// <summary>
        /// Must be locked before access.
        /// 
        /// All enqueued actions must handle their own exceptions and
        /// returned result (if any).  This is handled on the
        /// Invoke / InvokeAsync side.
        /// </summary>
        readonly Queue<Action> queue = new Queue<Action>();

        volatile bool queueIsDirty = false;


        void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
            }
        }


        void OnDestroy()
        {
            if (!ReferenceEquals(this, Instance))
            {
                return;
            }

            Instance = null;

            // Force the queue to be drained so that we don't have
            // any threads blocked in Invoke.
            FlushQueue_();
        }


        public static void InvokeAsync(Action action)
        {
            Instance.Enqueue(() =>
            {
                try
                {
                    action();
                }
                catch (Exception err)
                {
                    Debug.LogErrorFormat("Ignoring error in InvokeAsync: {0}", err);
                }
            });
        }


        public static void Invoke(Action action)
        {
            Instance.Invoke_(() =>
            {
                action();
                return null;
            });
        }

        public static T Invoke<T>(Func<T> action)
        {
            return (T)Instance.Invoke_(() => action());
        }

        object Invoke_(Func<object> action)
        {
            object result = null;
            Exception exn = null;
            bool done = false;

            Enqueue(() =>
            {
                try
                {
                    result = action();
                }
                catch (Exception err)
                {
                    exn = err;
                }
                finally
                {
                    done = true;
                }
            });

            while (true)
            {
                lock (queue)
                {
                    if (done)
                    {
                        if (exn == null)
                        {
                            return result;
                        }
                        else
                        {
                            throw exn;
                        }
                    }
                    Monitor.Wait(queue);
                }
            }
        }


        void Enqueue(Action action)
        {
            lock (queue)
            {
                queue.Enqueue(action);
                queueIsDirty = true;
            }
        }


        public static void FlushQueue()
        {
            Instance?.FlushQueue_();
        }

        void FlushQueue_()
        {
            EMAssert.OnMainThread();
            queueIsDirty = true;
            Update();
        }


        void Update()
        {
            if (!queueIsDirty)
            {
                return;
            }

            while (true)
            {
                Action action;
                lock (queue)
                {
                    if (!queue.TryDequeue(out action))
                    {
                        queueIsDirty = false;
                        Monitor.PulseAll(queue);
                        return;
                    }
                }
                action();
            }
        }
    }
}
