using System;
using NUnit.Framework;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Render.Label.Helper;
using System.Collections.Generic;
using RaphaelLibrary.Code.Render.Label.Renderer;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using RaphaelLibrary.Code.Render.Label.Manager;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.Renderer
{
    public class LabelReferenceRendererTest : TestBase
    {
        private const string S_FILE_PATH = @".\RaphaelLibrary\Render\Label\Renderer\TestFile\ValidStructure.txt";

        [Test]
        [TestCase("%%%<Reference StructureId=\"TestId\"/>%%%", true)]
        [TestCase("%%%<Reference StructureId=\"WrongId\"/>%%%", false)]
        [TestCase("%%%<Reference />%%%", false)]
        [TestCase("%%%<Reference />%%% StructureId=\"TestId\"/>%%%", false)]
        [TestCase("%%%<Reference %%%<Reference StructureId=\"TestId\"/>%%%", false)]
        public void TestReadLine(string line, bool expectedRes)
        {
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            SetupLabelStructureManager(deserializer);

            try
            {
                var renderer = new LabelReferenceRenderer(0);
                var actualRes = renderer.ReadLine(line, deserializer, LabelElementHelper.S_REFERENCE);

                Assert.AreEqual(expectedRes, actualRes);
                if (expectedRes)
                {
                    var placeHolders = GetPrivateField<List<PlaceHolderBase>>(renderer, "PlaceHolders");

                    Assert.AreEqual(1, placeHolders.Count);
                    var placeHolder = placeHolders[0] as ReferencePlaceHolder;
                    Assert.IsNotNull(placeHolder);

                    LabelStructureManager.Instance.TryGetLabelStructure("TestId", out var expectedStructure);
                    var actualStructure = GetPrivateField<IStructure>(placeHolder, "_labelStructure");

                    AssertHelper.AssertObject(expectedStructure, actualStructure);
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
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            SetupLabelStructureManager(deserializer);

            var line = "%%%<Reference StructureId=\"TestId\"/>%%%";
            var renderer = new LabelReferenceRenderer(0);

            try
            {
                var isSuccess = renderer.ReadLine(line, deserializer, LabelElementHelper.S_REFERENCE);
                Assert.IsTrue(isSuccess);

                var cloned = renderer.Clone();
                AssertHelper.AssertObject(renderer, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void TestTryRenderLabel()
        {
            var messageId = Guid.NewGuid();
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            SetupLabelStructureManager(deserializer);

            var renderer = new LabelReferenceRenderer(0);
            var line = "%%%<Reference StructureId=\"TestId\"/>%%%";

            try
            {
                var isSuccess = renderer.ReadLine(line, deserializer, LabelElementHelper.S_REFERENCE);
                Assert.IsTrue(isSuccess);

                var manager = new LabelManager(new[] { line }, messageId);
                isSuccess = renderer.TryRenderLabel(manager);

                var expected = $"{DateTime.UtcNow:dd-MM-yyyy}\r\n";

                Assert.IsTrue(isSuccess);
                Assert.AreEqual(expected, manager.Lines[0]);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        #region Helper

        public void SetupLabelStructureManager(LabelDeserializeHelper deserializer)
        {
            var expectedStructure = new LabelStructure("TestId", deserializer, LabelElementHelper.LABEL_RENDERER);
            var filePath = S_FILE_PATH;
            expectedStructure.ReadFile(filePath);

            var labelStructureList = GetPrivateField<Dictionary<string, IStructure>>(LabelStructureManager.Instance, "_labelStructureList");
            labelStructureList.Add(expectedStructure.Id, expectedStructure);
        }

        #endregion
    }
}