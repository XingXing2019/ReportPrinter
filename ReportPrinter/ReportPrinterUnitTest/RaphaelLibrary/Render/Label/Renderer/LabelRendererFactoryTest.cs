using System;
using NUnit.Framework;
using RaphaelLibrary.Code.Render.Label.Renderer;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.Renderer
{
    public class LabelRendererFactoryTest
    {
        [Test]
        [TestCase("Sql", typeof(LabelSqlRenderer))]
        [TestCase("Timestamp", typeof(LabelTimestampRenderer))]
        [TestCase("SqlVariable", typeof(LabelSqlVariableRenderer))]
        [TestCase("Reference", typeof(LabelReferenceRenderer))]
        [TestCase("Validation", typeof(LabelValidationRenderer))]
        [TestCase("InvalidName", null)]
        public void TestCreateLabelRenderer(string name, Type expectedType)
        {
            var lineIndex = 0;

            try
            {
                var labelRenderer = LabelRendererFactory.CreateLabelRenderer(name, lineIndex);
                Assert.AreEqual(expectedType, labelRenderer.GetType());
            }
            catch (InvalidOperationException ex)
            {
                var expectedError = $"Invalid name: {name} for label renderer";
                Assert.AreEqual(expectedError, ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}