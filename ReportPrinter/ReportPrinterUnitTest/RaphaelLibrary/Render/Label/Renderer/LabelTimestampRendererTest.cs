using System;
using System.Collections.Generic;
using NUnit.Framework;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.Manager;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using RaphaelLibrary.Code.Render.Label.Renderer;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.Renderer
{
    public class LabelTimestampRendererTest : TestBase
    {
        [Test]
        [TestCase("%%%<Timestamp Mask=\"yyyy-MM-dd\" IsUTC=\"true\"/>%%%", true)]
        [TestCase("%%%<Timestamp Mask=\"yyyy-MM-dd\" %%%<Timestamp IsUTC=\"true\"/>%%%", false)]
        [TestCase("%%%<Timestamp Mask=\"yyyy-MM-dd\"/>%%% IsUTC=\"true\"/>%%%", false)]
        [TestCase("%%%<Timestamp%%%<Timestamp Mask=\"yyyy-MM-dd\" IsUTC=\"true\"/>%%%", false)]
        [TestCase("%%%<Timestamp Mask=\"yyyy-MM-dd\" IsUTC=\"true\"", false)]
        public void TestReadLine(string line, bool expectedRes)
        {
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            var renderer = new LabelTimestampRenderer(0);

            try
            {
                var actualRes = renderer.ReadLine(line, deserializer, LabelElementHelper.S_TIMESTAMP);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    var placeHolders = GetPrivateField<List<PlaceHolderBase>>(renderer, "PlaceHolders");

                    Assert.AreEqual(1, placeHolders.Count);
                    var placeHolder = placeHolders[0] as TimestampPlaceHolder;
                    Assert.IsNotNull(placeHolder);

                    var mask = GetPrivateField<string>(placeHolder, "_mask");
                    var isUtc = GetPrivateField<bool>(placeHolder, "_isUtc");

                    Assert.AreEqual("yyyy-MM-dd", mask);
                    Assert.IsTrue(isUtc);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void TestClone()
        {
            var line = "%%%<Timestamp Mask=\"yyyy-MM-dd\" IsUTC=\"true\"/>%%%";
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            var renderer = new LabelTimestampRenderer(0);

            try
            {
                var isSuccess = renderer.ReadLine(line, deserializer, LabelElementHelper.S_TIMESTAMP);
                Assert.IsTrue(isSuccess);

                var cloned = renderer.Clone();
                AssertObject(renderer, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase("%%%<Timestamp Mask=\"yyyy-MM-dd\" IsUTC=\"true\"/>%%%", "yyyy-MM-dd", true)]
        [TestCase("%%%<Timestamp Mask=\"yyyy-MM-dd\" IsUTC=\"false\"/>%%%", "yyyy-MM-dd", false)]
        [TestCase("%%%<Timestamp Mask=\"dd-MM-yyyy\" IsUTC=\"true\"/>%%%", "dd-MM-yyyy", true)]
        [TestCase("%%%<Timestamp Mask=\"dd/MM/yyyy\" IsUTC=\"true\"/>%%%", "dd/MM/yyyy", true)]
        public void TestTryRenderLabel(string line, string mask, bool isUtc)
        {
            var messageId = Guid.NewGuid();
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            var renderer = new LabelTimestampRenderer(0);

            try
            {
                var isSuccess = renderer.ReadLine(line, deserializer, LabelElementHelper.S_TIMESTAMP);
                Assert.IsTrue(isSuccess);
                
                var manager = new LabelManager(new[] { line }, messageId);
                isSuccess = renderer.TryRenderLabel(manager);

                var expected = isUtc ? DateTime.UtcNow.ToString(mask) : DateTime.Now.ToString(mask);

                Assert.IsTrue(isSuccess);
                Assert.AreEqual(expected, manager.Lines[0]);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}