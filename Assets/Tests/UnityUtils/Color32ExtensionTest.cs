using System.Collections;

using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;


namespace EMUtils.UnityUtils
{
    public class Color32ExtensionTest
    {
        [Test]
        public void TestAlphaOverlayOpaqueBack()
        {
            var back = new Color32(128, 64, 192, 255);
            var front = new Color32(0, 255, 0, 64);
            var expected = new Color32(96, 112, 144, 255);
            Assert.AreEqual(expected, back.AlphaOverlay(front));
        }

        [Test]
        public void TestAlphaOverlayOpaqueFront()
        {
            var back = new Color32(128, 64, 192, 28);
            var front = new Color32(0, 255, 0, 255);
            var expected = front;
            Assert.AreEqual(expected, back.AlphaOverlay(front));
        }

        [Test]
        public void TestAlphaOverlayTransparentBack()
        {
            var back = new Color32(128, 64, 192, 0);
            var front = new Color32(0, 255, 0, 64);
            var expected = front;
            Assert.AreEqual(expected, back.AlphaOverlay(front));
        }

        [Test]
        public void TestAlphaOverlayTransparentFront()
        {
            var back = new Color32(128, 64, 192, 28);
            var front = new Color32(0, 255, 0, 0);
            var expected = back;
            Assert.AreEqual(expected, back.AlphaOverlay(front));
        }

        [Test]
        public void TestAlphaOverlayTranslucentBack()
        {
            var back = new Color32(128, 64, 192, 128);
            var front = new Color32(0, 255, 0, 64);
            var expected = new Color32(77, 140, 115, 160);
            Assert.AreEqual(expected, back.AlphaOverlay(front));
        }
    }
}
