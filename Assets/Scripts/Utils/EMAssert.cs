using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using UnityEngine.Assertions;


namespace EMUtils.Utils
{
    public static class EMAssert
    {
#if DEBUG
        private static int mainThreadId;
#endif

        [Conditional("DEBUG")]
        public static void OnMainThread()
        {
#if DEBUG
            if (mainThreadId == 0)
            {
                mainThreadId = Thread.CurrentThread.ManagedThreadId;
            }
            Assert.AreEqual(Thread.CurrentThread.ManagedThreadId, mainThreadId);
#endif
        }


        public static void IsNullOrEmpty<T>(IEnumerable<T> e)
        {
            Assert.IsTrue(e.IsNullOrEmpty());
        }

        public static void IsNotEmpty<T>(IEnumerable<T> e)
        {
            Assert.IsFalse(e.IsNullOrEmpty());
        }

        public static void AreReferenceEquals(object a, object b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return;
            }
            Assert.IsTrue(
                object.ReferenceEquals(a, b),
                string.Format("Object.ReferenceEquals(a, b) not true: a == {0}, b == {1}.", a, b));
        }

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use AreReferenceEquals", true)]
        public new static bool ReferenceEquals(object obj1, object obj2)
        {
            Assert.IsTrue(false);
            return false;
        }
    }
}
