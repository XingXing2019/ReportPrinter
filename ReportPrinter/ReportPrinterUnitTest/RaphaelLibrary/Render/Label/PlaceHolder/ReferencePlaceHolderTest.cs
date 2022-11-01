using System;
using System.IO;
using NUnit.Framework;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.PlaceHolder
{
    public class ReferencePlaceHolderTest : PlaceHolderTestBase
    {
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestTryGetPlaceHolderValue(bool expectedRes)
        {
            var filePath = @".\RaphaelLibrary\Render\Label\PlaceHolder\TestFile\LabelStructure.txt";

            if (!expectedRes)
            {
                var invalidLine = "%%%<SqlVariable Name=\"VariableName\" />%%%";
                filePath = AppendLineToTxtFile(filePath, invalidLine);
            }

            var id = "TestId";
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            var labelStructure = new LabelStructure(id, deserializer, LabelElementHelper.LABEL_RENDERER);

            var isSuccess = labelStructure.ReadFile(filePath);
            Assert.IsTrue(isSuccess);

            var manager = CreateLabelManager(S_PLACE_HOLDER, Guid.NewGuid());

            try
            {
                var referencePlaceHolder = new ReferencePlaceHolder(S_PLACE_HOLDER, labelStructure);
                var actualRes = referencePlaceHolder.TryReplacePlaceHolder(manager, 0);
                var now = DateTime.UtcNow.ToString("dd-MM-yyyy hh:mm:ss\r\n");

                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    Assert.AreEqual(now, manager.Lines[0]);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                if (!expectedRes)
                {
                    File.Delete(filePath);
                }
            }
        }

        [Test]
        public void TestClone()
        {
            var filePath = @".\RaphaelLibrary\Render\Label\PlaceHolder\TestFile\LabelStructure.txt";
            var id = "TestId";
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            var labelStructure = new LabelStructure(id, deserializer, LabelElementHelper.LABEL_RENDERER);

            var isSuccess = labelStructure.ReadFile(filePath);
            Assert.IsTrue(isSuccess);


            try
            {
                var referencePlaceHolder = new ReferencePlaceHolder(S_PLACE_HOLDER, labelStructure);
                var cloned = referencePlaceHolder.Clone();

                AssertObject(referencePlaceHolder, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}