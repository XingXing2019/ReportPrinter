using System;
using NUnit.Framework;
using RaphaelLibrary.Code.Render.Label.Manager;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.PlaceHolder
{
    public class TimestampPlaceHolderTest : TestBase
    {
        [Test]
        [TestCase(true, true, "dd-MM-yyyy")]
        [TestCase(true, true, "dd-MM-yyyy hh:mm:ss")]
        [TestCase(true, false, "dd-MM-yyyy hh:mm:ss")]
        [TestCase(true, true, "hh:mm:ss")]
        public void TestTryReplacePlaceHolder(bool expectedRes, bool isUtc, string mask)
        {
            var placeholder = "%%%<Timestamp Mask=\"dd-MM-yyyy\" IsUTC=\"true\"/>%%%";
            var timestamp = new TimestampPlaceHolder(placeholder, isUtc, mask);

            var lines = new string[] { placeholder };
            var manager = new LabelManager(lines, Guid.NewGuid());

            try
            {
                var actualRes = timestamp.TryReplacePlaceHolder(manager, 0);
                var expectedValue = isUtc ? DateTime.UtcNow.ToString(mask) : DateTime.Now.ToString(mask);
                
                Assert.AreEqual(expectedRes, actualRes);
                Assert.AreEqual(manager.Lines[0], expectedValue);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [Test]
        public void TestClone()
        {
            var placeholder = "%%%<Timestamp Mask=\"dd-MM-yyyy\" IsUTC=\"true\"/>%%%";
            var isUtc = true;
            var mask = "dd-MM-yyyy";
            var timestamp = new TimestampPlaceHolder(placeholder, isUtc, mask);

            try
            {
                var cloned = timestamp.Clone();
                AssertObject(timestamp, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}