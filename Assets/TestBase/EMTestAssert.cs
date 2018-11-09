using NUnit.Framework;
using UnityEngine;

using EMUtils.UnityUtils;


namespace EMUtils.TestBase
{
    public static class EMTestAssert
    {
        public static void AreEqual(Vector3 expected, Vector3 actual, float deltaSquared = 1e-8f)
        {
            Assert.IsTrue(Vector3Extension.SquaredDistance(expected, actual) < deltaSquared,
                          "Vector3's are not equal: expected={0}; actual={1}; deltaSquared={2}",
                          expected.ToString("F8"), actual.ToString("F8"), deltaSquared);
        }
    }
}
