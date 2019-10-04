using System;

using EMUtils.Utils;

using NUnit.Framework;


namespace EMUtils.UnityUtils
{
    public class GuidExtensionTest
    {
        [Test]
        public void TestFromStringBase64url()
        {
            var i = "X2JWenTCTTK2SdJtdiQzvw";
            var e = "5F62567A-74C2-4D32-B649-D26D762433BF";
            DoTestFromStringBase64url(i, e);
        }

        [Test]
        public void TestFromStringBase64urlDashUnderscore()
        {
            var i = "LhM2_UNJSF-Lrd9FuSrjLg";
            var e = "2e1336fd-4349-485f-8bad-df45b92ae32e";
            DoTestFromStringBase64url(i, e);
        }

        [Test]
        public void TestToStringBase64url()
        {
            var i = "5F62567A-74C2-4D32-B649-D26D762433BF";
            var e = "X2JWenTCTTK2SdJtdiQzvw";
            DoTestToStringBase64url(i, e);
        }

        [Test]
        public void TestToStringBase64urlDashUnderscore()
        {
            var i = "2e1336fd-4349-485f-8bad-df45b92ae32e";
            var e = "LhM2_UNJSF-Lrd9FuSrjLg";
            DoTestToStringBase64url(i, e);
        }

        private static void DoTestFromStringBase64url(string i, string e)
        {
            var g = new Guid(e);
            Assert.AreEqual(g, GuidExtension.FromStringBase64url(i));
        }

        private static void DoTestToStringBase64url(string i, string e)
        {
            var g = new Guid(i);
            Assert.AreEqual(e, g.ToStringBase64url());
        }
    }
}
