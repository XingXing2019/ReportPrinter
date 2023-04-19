using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PdfSharp.Drawing;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfRendererBaseManagerTest : DatabaseTestBase<PdfRendererBaseModel>
    {
        public PdfRendererBaseManagerTest()
        {
            Manager = new PdfRendererBaseEFCoreManager();
        }

        [TearDown]
        public new void TearDown()
        {
            Manager.DeleteAll().Wait();
        }

        [Test]
        [TestCase(typeof(PdfRendererBaseEFCoreManager))]
        [TestCase(typeof(PdfRendererBaseSPManager))]
        public async Task TestPdfRendererBaseManager_Post(Type managerType)
        {
            try
            {
                var mgr = (IPdfRendererBaseManager)Activator.CreateInstance(managerType);

                var pdfRendererId = Guid.NewGuid();
                var expectedPdfRenderer = new PdfRendererBaseModel
                {
                    PdfRendererBaseId = pdfRendererId,
                    Id = "Test Pdf Barcode Renderer 1",
                    RendererType = PdfRendererType.Barcode,
                    Margin = null,
                    Padding = null,
                    HorizontalAlignment = null,
                    VerticalAlignment = null,
                    Position = null,
                    Left = null,
                    Right = null,
                    Top = null,
                    Bottom = null,
                    FontSize = null,
                    FontFamily = null,
                    FontStyle = null,
                    Opacity = null,
                    BrushColor = null,
                    BackgroundColor = null,
                    Row = 2,
                    Column = 4,
                    RowSpan = null,
                    ColumnSpan = null,
                };

                await mgr.Post(expectedPdfRenderer);

                var actualPdfRenderer = await mgr.Get(pdfRendererId);
                Assert.IsNotNull(actualPdfRenderer);
                AssertHelper.AssertObject(expectedPdfRenderer, actualPdfRenderer);

                await mgr.Delete(pdfRendererId);
                actualPdfRenderer = await mgr.Get(pdfRendererId);

                Assert.IsNull(actualPdfRenderer);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(typeof(PdfRendererBaseEFCoreManager))]
        [TestCase(typeof(PdfRendererBaseSPManager))]
        public async Task TestPdfRendererBaseManager_Get(Type managerType)
        {
            try
            {
                var mgr = (IPdfRendererBaseManager)Activator.CreateInstance(managerType);

                var pdfRendererId = Guid.NewGuid();
                var expectedPdfRenderer = new PdfRendererBaseModel
                {
                    PdfRendererBaseId = pdfRendererId,
                    Id = "Test Pdf Barcode Renderer 1",
                    RendererType = PdfRendererType.Barcode,
                    Margin = "10, 5, 10, 5",
                    Padding = "5, 10, 5, 10",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Position = Position.Static,
                    Left = 10.5,
                    Right = 5.5,
                    Top = 20.5,
                    Bottom = 15.5,
                    FontSize = 6.5,
                    FontFamily = "Time New Roman",
                    FontStyle = XFontStyle.Strikeout,
                    Opacity = 0.9,
                    BrushColor = XKnownColor.YellowGreen,
                    BackgroundColor = XKnownColor.Brown,
                    Row = 2,
                    Column = 4,
                    RowSpan = 5,
                    ColumnSpan = 9,
                };

                await mgr.Post(expectedPdfRenderer);

                var actualPdfRenderer = await mgr.Get(pdfRendererId);
                Assert.IsNotNull(actualPdfRenderer);
                AssertHelper.AssertObject(expectedPdfRenderer, actualPdfRenderer);

                await mgr.Delete(pdfRendererId);
                actualPdfRenderer = await mgr.Get(pdfRendererId);

                Assert.IsNull(actualPdfRenderer);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(typeof(PdfRendererBaseEFCoreManager))]
        [TestCase(typeof(PdfRendererBaseSPManager))]
        public async Task TestPdfRendererBaseManager_GetAll(Type managerType)
        {
            try
            {
                var mgr = (IPdfRendererBaseManager)Activator.CreateInstance(managerType);
                var expectedPdfRenderers = new List<PdfRendererBaseModel>();
                var pdfRenderersToDelete = new List<Guid>();

                for (int i = 0; i < 10; i++)
                {
                    var pdfRendererId = Guid.NewGuid();
                    var expectedPdfRenderer = new PdfRendererBaseModel
                    {
                        PdfRendererBaseId = pdfRendererId,
                        Id = $"Test PDF Renderer {i + 1}",
                        RendererType = PdfRendererType.Text,
                        Margin = "10, 5, 10, 5",
                        Padding = "5, 10, 5, 10",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Position = Position.Static,
                        Left = 10.5,
                        Right = 5.5,
                        Top = 20.5,
                        Bottom = 15.5,
                        FontSize = 6.5,
                        FontFamily = "Time New Roman",
                        FontStyle = XFontStyle.Strikeout,
                        Opacity = 0.9,
                        BrushColor = XKnownColor.YellowGreen,
                        BackgroundColor = XKnownColor.Brown,
                        Row = 2,
                        Column = 4,
                        RowSpan = 5,
                        ColumnSpan = 9,
                    };

                    await mgr.Post(expectedPdfRenderer);
                    expectedPdfRenderers.Add(expectedPdfRenderer);

                    if (i < 5)
                    {
                        pdfRenderersToDelete.Add(pdfRendererId);
                    }
                }

                var actualPdfRenderers = await mgr.GetAll();
                Assert.AreEqual(10, actualPdfRenderers.Count);

                foreach (var expectedPdfRenderer in expectedPdfRenderers)
                {
                    var actualPdfRenderer = actualPdfRenderers.Single(x => x.PdfRendererBaseId == expectedPdfRenderer.PdfRendererBaseId);
                    AssertHelper.AssertObject(expectedPdfRenderer, actualPdfRenderer);
                }

                await mgr.Delete(pdfRenderersToDelete);
                actualPdfRenderers = await mgr.GetAll();

                Assert.AreEqual(5, actualPdfRenderers.Count);
                expectedPdfRenderers = expectedPdfRenderers.Where(x => !pdfRenderersToDelete.Contains(x.PdfRendererBaseId)).ToList();

                foreach (var expectedPdfRenderer in expectedPdfRenderers)
                {
                    var actualPdfRenderer = actualPdfRenderers.Single(x => x.PdfRendererBaseId == expectedPdfRenderer.PdfRendererBaseId);
                    AssertHelper.AssertObject(expectedPdfRenderer, actualPdfRenderer);
                }

                await mgr.DeleteAll();
                actualPdfRenderers = await mgr.GetAll();
                Assert.AreEqual(0, actualPdfRenderers.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(typeof(PdfRendererBaseEFCoreManager))]
        [TestCase(typeof(PdfRendererBaseSPManager))]
        public async Task TestPdfRendererBaseManager_GetAllByRendererType(Type managerType)
        {
            try
            {
                var mgr = (IPdfRendererBaseManager)Activator.CreateInstance(managerType);
                var expectedTextRenderers = new List<PdfRendererBaseModel>();

                for (int i = 0; i < 5; i++)
                {
                    var pdfRendererId = Guid.NewGuid();
                    var textRender = new PdfRendererBaseModel
                    {
                        PdfRendererBaseId = pdfRendererId,
                        Id = $"PDF Text Renderer {i + 1}",
                        RendererType = PdfRendererType.Text,
                        Margin = "10, 5, 10, 5",
                        Padding = "5, 10, 5, 10",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Position = Position.Static,
                        Left = 10.5,
                        Right = 5.5,
                        Top = 20.5,
                        Bottom = 15.5,
                        FontSize = 6.5,
                        FontFamily = "Time New Roman",
                        FontStyle = XFontStyle.Strikeout,
                        Opacity = 0.9,
                        BrushColor = XKnownColor.YellowGreen,
                        BackgroundColor = XKnownColor.Brown,
                        Row = 2,
                        Column = 4,
                        RowSpan = 5,
                        ColumnSpan = 9,
                    };

                    await mgr.Post(textRender);
                    expectedTextRenderers.Add(textRender);
                }

                for (int i = 0; i < 5; i++)
                {
                    var pdfRendererId = Guid.NewGuid();
                    var imageRender = new PdfRendererBaseModel
                    {
                        PdfRendererBaseId = pdfRendererId,
                        Id = $"PDF Image Renderer {i + 1}",
                        RendererType = PdfRendererType.Image,
                        Margin = "10, 5, 10, 5",
                        Padding = "5, 10, 5, 10",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Position = Position.Static,
                        Left = 10.5,
                        Right = 5.5,
                        Top = 20.5,
                        Bottom = 15.5,
                        FontSize = 6.5,
                        FontFamily = "Time New Roman",
                        FontStyle = XFontStyle.Strikeout,
                        Opacity = 0.9,
                        BrushColor = XKnownColor.YellowGreen,
                        BackgroundColor = XKnownColor.Brown,
                        Row = 2,
                        Column = 4,
                        RowSpan = 5,
                        ColumnSpan = 9,
                    };

                    await mgr.Post(imageRender);
                }

                var actualTextRenderers = await mgr.GetAllByRendererType(PdfRendererType.Text);
                Assert.AreEqual(5, actualTextRenderers.Count);

                foreach (var expectedTextRenderer in expectedTextRenderers)
                {
                    var actualPdfRenderer = actualTextRenderers.Single(x => x.PdfRendererBaseId == expectedTextRenderer.PdfRendererBaseId);
                    AssertHelper.AssertObject(expectedTextRenderer, actualPdfRenderer);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(typeof(PdfRendererBaseEFCoreManager))]
        [TestCase(typeof(PdfRendererBaseSPManager))]
        public async Task TestPdfRendererBaseManager_GetAlByRendererIdPrefix(Type managerType)
        {
            try
            {
                var mgr = (IPdfRendererBaseManager)Activator.CreateInstance(managerType);
                var expectedTestRenderers = new List<PdfRendererBaseModel>();
                var expectedRealRenderers = new List<PdfRendererBaseModel>();

                for (int i = 0; i < 10; i++)
                {
                    var pdfRendererId = Guid.NewGuid();
                    var testRender = new PdfRendererBaseModel
                    {
                        PdfRendererBaseId = pdfRendererId,
                        Id = $"Test Renderer {i + 1}",
                        RendererType = PdfRendererType.Text,
                        Margin = "10, 5, 10, 5",
                        Padding = "5, 10, 5, 10",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Position = Position.Static,
                        Left = 10.5,
                        Right = 5.5,
                        Top = 20.5,
                        Bottom = 15.5,
                        FontSize = 6.5,
                        FontFamily = "Time New Roman",
                        FontStyle = XFontStyle.Strikeout,
                        Opacity = 0.9,
                        BrushColor = XKnownColor.YellowGreen,
                        BackgroundColor = XKnownColor.Brown,
                        Row = 2,
                        Column = 4,
                        RowSpan = 5,
                        ColumnSpan = 9,
                    };

                    await mgr.Post(testRender);
                    expectedTestRenderers.Add(testRender);
                }

                for (int i = 0; i < 5; i++)
                {
                    var pdfRendererId = Guid.NewGuid();
                    var realRender = new PdfRendererBaseModel
                    {
                        PdfRendererBaseId = pdfRendererId,
                        Id = $"Real Renderer {i + 1}",
                        RendererType = PdfRendererType.Text,
                        Margin = "10, 5, 10, 5",
                        Padding = "5, 10, 5, 10",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Position = Position.Static,
                        Left = 10.5,
                        Right = 5.5,
                        Top = 20.5,
                        Bottom = 15.5,
                        FontSize = 6.5,
                        FontFamily = "Time New Roman",
                        FontStyle = XFontStyle.Strikeout,
                        Opacity = 0.9,
                        BrushColor = XKnownColor.YellowGreen,
                        BackgroundColor = XKnownColor.Brown,
                        Row = 2,
                        Column = 4,
                        RowSpan = 5,
                        ColumnSpan = 9,
                    };

                    await mgr.Post(realRender);
                    expectedRealRenderers.Add(realRender);
                }

                var rendererPrefix = "Test Renderer";
                var actualRenders = await mgr.GetAlByRendererIdPrefix(rendererPrefix);
                Assert.AreEqual(10, actualRenders.Count);

                foreach (var expectedTestRender in expectedTestRenderers)
                {
                    var actualPdfRenderer = actualRenders.Single(x => x.PdfRendererBaseId == expectedTestRender.PdfRendererBaseId);
                    AssertHelper.AssertObject(expectedTestRender, actualPdfRenderer);
                }

                rendererPrefix = "Real Renderer";
                actualRenders = await mgr.GetAlByRendererIdPrefix(rendererPrefix);
                Assert.AreEqual(5, actualRenders.Count);

                foreach (var expectedRealRender in expectedRealRenderers)
                {
                    var actualPdfRenderer = actualRenders.Single(x => x.PdfRendererBaseId == expectedRealRender.PdfRendererBaseId);
                    AssertHelper.AssertObject(expectedRealRender, actualPdfRenderer);
                }

                rendererPrefix = "Invalid Render";
                actualRenders = await mgr.GetAlByRendererIdPrefix(rendererPrefix);
                Assert.AreEqual(0, actualRenders.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}