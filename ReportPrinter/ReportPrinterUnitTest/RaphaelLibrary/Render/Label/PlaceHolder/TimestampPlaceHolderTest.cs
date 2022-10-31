using System;
using NUnit.Framework;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.PlaceHolder
{
    public class TimestampPlaceHolderTest : PlaceHolderTestBase
    {
        private const string S_PLACE_HOLDER = "%%%<Timestamp Mask=\"dd-MM-yyyy\" IsUTC=\"true\"/>%%%";
        
        [Test]
        [TestCase(true, true, "dd-MM-yyyy")]
        [TestCase(true, true, "dd-MM-yyyy hh:mm:ss")]
        [TestCase(true, false, "dd-MM-yyyy hh:mm:ss")]
        [TestCase(true, true, "hh:mm:ss")]
        public void TestTryReplacePlaceHolder(bool expectedRes, bool isUtc, string mask)
        {
            var manager = CreateLabelManager(S_PLACE_HOLDER, Guid.NewGuid());

            try
            {
                var timestamp = new TimestampPlaceHolder(S_PLACE_HOLDER, isUtc, mask);
                var actualRes = timestamp.TryReplacePlaceHolder(manager, 0);
                var expectedValue = isUtc ? DateTime.UtcNow.ToString(mask) : DateTime.Now.ToString(mask);
                
                Assert.AreEqual(expectedRes, actualRes);
                Assert.AreEqual(expectedValue, manager.Lines[0]);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [Test]
        public void TestClone()
        {
            var isUtc = true;
            var mask = "dd-MM-yyyy";
            var timestamp = new TimestampPlaceHolder(S_PLACE_HOLDER, isUtc, mask);

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