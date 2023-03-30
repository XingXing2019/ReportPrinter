using System;
using System.Collections.Generic;
using NUnit.Framework;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.PDF.Renderer;
using RaphaelLibrary.Code.Render.PDF.Structure;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.PDF.Helper
{
    public class LayoutHelperTest : TestBase
    {
        private static readonly object[] TryCreateContentBoxTextSizeTestCases =
        {
            new object[] {new BoxModel(0, 0, 100, 50), HorizontalAlignment.Left, true, new BoxModel(10, 20, 50, 10)},
            new object[] {new BoxModel(0, 0, 100, 50), HorizontalAlignment.Center, true, new BoxModel(25, 20, 50, 10)},
            new object[] {new BoxModel(0, 0, 100, 50), HorizontalAlignment.Right, true, new BoxModel(40, 20, 50, 10)},
            new object[] {new BoxModel(0, 0, 50, 50), HorizontalAlignment.Right, false, null},
            new object[] {new BoxModel(0, 0, 100, 10), HorizontalAlignment.Right, false, null},
        };

        [Test]
        [TestCaseSource(nameof(TryCreateContentBoxTextSizeTestCases))]
        public void TestTryCreateContentBox(BoxModel container, HorizontalAlignment horizontalAlignment, bool expectedRes, BoxModel expectedModel)
        {
            var textSize = new XSize(50, 10);
            var pdfRender = CreatePdfRenderer();

            try
            {
                var actualRes = LayoutHelper.TryCreateContentBox(container, textSize, pdfRender, out var actualModel, horizontalAlignment);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    Assert.AreEqual(expectedModel.X, Math.Round(actualModel.X, 3));
                    Assert.AreEqual(expectedModel.Y, Math.Round(actualModel.Y, 3));
                    Assert.AreEqual(expectedModel.Height, Math.Round(actualModel.Height, 3));
                    Assert.AreEqual(expectedModel.Width, Math.Round(actualModel.Width, 3));
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        private static readonly object[] TryCreateMarginPaddingTestCases =
        {
            new object[] { "1.5 2.5 3.2 2.1", true, new MarginPaddingModel(1.5, 2.5, 3.2, 2.1) },
            new object[] { "Invalid Input", false, new MarginPaddingModel(0, 0, 0, 0) },
            new object[] { "1.5, 2.5, 3.5", false, new MarginPaddingModel(0, 0, 0, 0) },
            new object[] { "a, 2.5, 3.5, 5.0", false, new MarginPaddingModel(0, 0, 0, 0) },
        };

        [Test]
        [TestCaseSource(nameof(TryCreateMarginPaddingTestCases))]
        public void TestTryCreateMarginPadding(string input, bool expectedRes, MarginPaddingModel expectedModel)
        {
            try
            {
                var actualRes = LayoutHelper.TryCreateMarginPadding(input, out var actualModel);
                Assert.AreEqual(expectedRes, actualRes);
                Assert.AreEqual(expectedModel.Top, actualModel.Top);
                Assert.AreEqual(expectedModel.Bottom, actualModel.Bottom);
                Assert.AreEqual(expectedModel.Left, actualModel.Left);
                Assert.AreEqual(expectedModel.Right, actualModel.Right);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        private static readonly object[] AdjustBoxLocationLayoutParameterTestCases =
        {
            new object[]
            {
                new LayoutParameter { Left = 1.5, Right = 2.5, Top = 3.2, Bottom = 2.1, Position = Position.Static },
                new BoxModel(10, 15, 100, 200)
            },
            new object[]
            {
                new LayoutParameter { Left = 1.5, Right = 2.5, Top = 3.2, Bottom = 2.1, Position = Position.Relative },
                new BoxModel(9, 16.1, 100, 200)
            }
        };

        [Test]
        [TestCaseSource(nameof(AdjustBoxLocationLayoutParameterTestCases))]
        public void TestAdjustBoxLocation(LayoutParameter layoutParameter, BoxModel expectedRes)
        {
            var box = new BoxModel(10, 15, 100, 200);
            try
            {
                var actualRes = LayoutHelper.AdjustBoxLocation(box, layoutParameter);
                Assert.AreEqual(expectedRes.X, Math.Round(actualRes.X, 3));
                Assert.AreEqual(expectedRes.Y, Math.Round(actualRes.Y, 3));
                Assert.AreEqual(expectedRes.Height, Math.Round(actualRes.Height, 3));
                Assert.AreEqual(expectedRes.Width, Math.Round(actualRes.Width, 3));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        private static readonly object[] AdjustBoxLocationLeftRightTestCases =
        {
            new object[] { Position.Static, 10.3, 5.2, new BoxModel(10, 15, 100, 200) },
            new object[] { Position.Relative, 10.3, 5.2, new BoxModel(15.1, 15, 100, 200) },
        };

        [Test]
        [TestCaseSource(nameof(AdjustBoxLocationLeftRightTestCases))]
        public void TestAdjustBoxLocation(Position position, double left, double right, BoxModel expectedRes)
        {
            var box = new BoxModel(10, 15, 100, 200);

            try
            {
                var actualRes = LayoutHelper.AdjustBoxLocation(box, position, left, right);
                Assert.AreEqual(expectedRes.X, Math.Round(actualRes.X, 3));
                Assert.AreEqual(expectedRes.Y, Math.Round(actualRes.Y, 3));
                Assert.AreEqual(expectedRes.Height, Math.Round(actualRes.Height, 3));
                Assert.AreEqual(expectedRes.Width, Math.Round(actualRes.Width, 3));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        private static readonly object[] CreateContainerTestCases =
        {
            new object[] { "A4", PdfStructure.PdfReportHeader, new BoxModel(15, 10, 565.0, 85.25) },
            new object[] { "A4", PdfStructure.PdfPageHeader, new BoxModel(15, 10, 565.0, 68.632) },
            new object[] { "A4", PdfStructure.PdfPageFooter, new BoxModel(15, 679.509, 565.0, 142.491) },
            new object[] { "A4", PdfStructure.PdfReportFooter, new BoxModel(15, 666.583, 565.0, 155.417) }
        };

        [Test]
        [TestCaseSource(nameof(CreateContainerTestCases))]
        public void TestCreateContainer(string pagSizeStr, PdfStructure location, BoxModel expectedRes)
        {
            try
            {
                PageHelper.TryGetPageSize(pagSizeStr, out var pageSize);
                var pdfStructureList = CreatePdfStructureList();

                var actualRes = LayoutHelper.CreateContainer(pageSize, location, pdfStructureList);

                Assert.AreEqual(expectedRes.X, Math.Round(actualRes.X, 3));
                Assert.AreEqual(expectedRes.Y, Math.Round(actualRes.Y, 3));
                Assert.AreEqual(expectedRes.Height, Math.Round(actualRes.Height, 3));
                Assert.AreEqual(expectedRes.Width, Math.Round(actualRes.Width, 3));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase("A4", PdfStructure.PdfReportHeader, 105.25)]
        [TestCase("A4", PdfStructure.PdfPageHeader, 88.632)]
        [TestCase("A4", PdfStructure.PdfPageBody, 0)]
        [TestCase("A4", PdfStructure.PdfPageFooter, 162.491)]
        [TestCase("A4", PdfStructure.PdfReportFooter, 175.417)]
        public void TestCalcPdfStructureHeight(string pagSizeStr, PdfStructure location, double expectedRes)
        {
            try
            {
                PageHelper.TryGetPageSize(pagSizeStr, out var pageSize);
                var pdfStructureList = CreatePdfStructureList();

                var actualRes = Math.Round(LayoutHelper.CalcPdfStructureHeight(pageSize, location, pdfStructureList), 3);
                Assert.AreEqual(expectedRes, actualRes);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        private static readonly object[] AllocateWordsTestCase =
        {
            new object[] { "Test Allocate Words", new List<string> { "Test", "Allocate", "Words" }, 2, 20 },
            new object[] { "Test Allocate Words", new List<string> { "Test A", "llocat", "e Word", "s" }, 3, 20 },
            new object[] { "Test Allocate Words", new List<string> { "Test Al", "locate ", "Words" }, 2, 15 },
            new object[] { "There are some test cases", new List<string> { "There are", "some test", "cases" }, 2, 20 },
            new object[] { "There are some test cases", new List<string> { "There", "are", "some", "test", "cases" }, 3, 20 },
            new object[] { "There are some test cases", new List<string> { "There", "are", "some", "test", "cases" }, 2, 15 },
            new object[] { "ThisIsAVeryLongSentence", new List<string> { "ThisIsAVer", "yLongSente", "nce" }, 2, 20 }
        };

        [Test]
        [TestCaseSource(nameof(AllocateWordsTestCase))]
        public void TestAllocateWords(string text, List<string> expectedRes, double widthPerLetter, double containerWidth)
        {
            try
            {
                var actualRes = LayoutHelper.AllocateWords(text, widthPerLetter, containerWidth);

                Assert.AreEqual(expectedRes.Count, actualRes.Count);
                for (int i = 0; i < expectedRes.Count; i++)
                {
                    Assert.AreEqual(expectedRes[i], actualRes[i]);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        #region Helper

        private Dictionary<PdfStructure, PdfStructureBase> CreatePdfStructureList()
        {
            var type = typeof(PdfStructureBase);

            var reportHeader = new PdfReportHeader(new HashSet<string>());
            SetPrivateProperty(type, reportHeader, "HeightRatio", 1.5);
            SetPrivateProperty(type, reportHeader, "Margin", new MarginPaddingModel(5, 10, 5, 10));
            SetPrivateProperty(type, reportHeader, "Padding", new MarginPaddingModel(5, 5, 5, 5));

            var pageHeader = new PdfPageHeader(new HashSet<string>());
            SetPrivateProperty(type, pageHeader, "HeightRatio", 1.2);
            SetPrivateProperty(type, pageHeader, "Margin", new MarginPaddingModel(5, 10, 5, 10));
            SetPrivateProperty(type, pageHeader, "Padding", new MarginPaddingModel(5, 5, 5, 5));

            var pageBody = new PdfPageBody(new HashSet<string>());
            SetPrivateProperty(type, pageBody, "HeightRatio", 8);
            SetPrivateProperty(type, pageBody, "Margin", new MarginPaddingModel(5, 10, 5, 10));
            SetPrivateProperty(type, pageBody, "Padding", new MarginPaddingModel(5, 5, 5, 5));

            var pageFooter = new PdfPageFooter(new HashSet<string>());
            SetPrivateProperty(type, pageFooter, "HeightRatio", 2.2);
            SetPrivateProperty(type, pageFooter, "Margin", new MarginPaddingModel(5, 10, 5, 10));
            SetPrivateProperty(type, pageFooter, "Padding", new MarginPaddingModel(5, 5, 5, 5));

            var reportFooter = new PdfReportFooter(new HashSet<string>());
            SetPrivateProperty(type, reportFooter, "HeightRatio", 2.5);
            SetPrivateProperty(type, reportFooter, "Margin", new MarginPaddingModel(5, 10, 5, 10));
            SetPrivateProperty(type, reportFooter, "Padding", new MarginPaddingModel(5, 5, 5, 5));

            var pdfStructureList = new Dictionary<PdfStructure, PdfStructureBase>
            {
                { PdfStructure.PdfReportHeader, reportHeader },
                { PdfStructure.PdfPageHeader, pageHeader },
                { PdfStructure.PdfPageBody, pageBody },
                { PdfStructure.PdfPageFooter, pageFooter },
                { PdfStructure.PdfReportFooter, reportFooter },
            };

            return pdfStructureList;
        }

        private PdfRendererBase CreatePdfRenderer()
        {
            var filePath = @".\RaphaelLibrary\Render\PDF\TestFile\PdfPageNumberRenderer.xml";
            var node = TestFileHelper.GetXmlNode(filePath);

            var pdfRenderer = new PdfPageNumberRenderer(PdfStructure.PdfPageFooter);
            pdfRenderer.ReadXml(node);

            return pdfRenderer;
        }

        #endregion
    }
}