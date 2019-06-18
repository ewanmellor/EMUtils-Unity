using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine.Assertions;


namespace EMUtils.Utils
{
    public static class EMAssert
    {
        private static int mainThreadId;

        public static void OnMainThread()
        {
            if (mainThreadId == 0)
            {
                mainThreadId = Thread.CurrentThread.ManagedThreadId;
            }
            Assert.AreEqual(Thread.CurrentThread.ManagedThreadId, mainThreadId);
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
            if (Object.ReferenceEquals(a, b))
            {
                return;
            }
            Assert.IsTrue(
                Object.ReferenceEquals(a, b),
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
