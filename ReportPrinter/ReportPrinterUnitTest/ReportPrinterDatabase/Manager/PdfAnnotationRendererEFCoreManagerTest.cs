using NUnit.Framework;
using PdfSharp.Drawing;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfBarcodeRenderer;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using System.Threading.Tasks;
using System;
using PdfSharp.Pdf.Annotations;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfAnnotationRenderer;
using ZXing;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfAnnotationRendererEFCoreManagerTest : DatabaseTestBase<PdfRendererBaseModel>
    {
        public PdfAnnotationRendererEFCoreManagerTest()
        {
            Manager = new PdfRendererBaseEFCoreManager();
        }

        [TearDown]
        public new void TearDown()
        {
            Manager.DeleteAll().Wait();
        }

        [Test]
        [TestCase(typeof(PdfAnnotationRendererEFCoreManager))]
        public async Task TestPdfBarcodeRendererManager_Get(Type managerType)
        {
            try
            {
                var mgr = (IPdfAnnotationRendererManager)Activator.CreateInstance(managerType);

                var rendererBaseId = Guid.NewGuid();

                var expectedRenderer = new PdfAnnotationRendererModel
                {
                    RendererBase = new PdfRendererBaseModel
                    {
                        PdfRendererBaseId = rendererBaseId,
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
                    },
                    AnnotationRendererType = AnnotationRendererType.Text,
                    Title = "Test Title 1",
                    Icon = PdfTextAnnotationIcon.Insert,
                    SqlTemplateId = "Test Sql Template 1",
                    SqlId = "Test Sql 1",
                    SqlResColumn = "Test Res Column 1"
                };

                await mgr.Post(expectedRenderer);

                var actualRenderer = await mgr.Get(rendererBaseId);
                Assert.IsNotNull(actualRenderer);
                AssertHelper.AssertObject(expectedRenderer, actualRenderer);

                expectedRenderer.RendererBase.Id = "Test Pdf Barcode Renderer 2";
                expectedRenderer.RendererBase.RendererType = PdfRendererType.Watermark;
                expectedRenderer.RendererBase.Margin = null;
                expectedRenderer.RendererBase.Padding = null;
                expectedRenderer.RendererBase.HorizontalAlignment = null;
                expectedRenderer.RendererBase.VerticalAlignment = null;
                expectedRenderer.RendererBase.Position = null;
                expectedRenderer.RendererBase.Left = null;
                expectedRenderer.RendererBase.Right = null;
                expectedRenderer.RendererBase.Top = null;
                expectedRenderer.RendererBase.Bottom = null;
                expectedRenderer.RendererBase.FontSize = null;
                expectedRenderer.RendererBase.FontFamily = null;
                expectedRenderer.RendererBase.FontStyle = null;
                expectedRenderer.RendererBase.Opacity = null;
                expectedRenderer.RendererBase.BrushColor = null;
                expectedRenderer.RendererBase.BackgroundColor = null;
                expectedRenderer.RendererBase.Row = 5;
                expectedRenderer.RendererBase.Column = 1;
                expectedRenderer.RendererBase.RowSpan = null;
                expectedRenderer.RendererBase.ColumnSpan = null;
                expectedRenderer.AnnotationRendererType = AnnotationRendererType.Sql;
                expectedRenderer.Title = null;
                expectedRenderer.Icon = null;
                expectedRenderer.SqlTemplateId = "Test Sql Template 2";
                expectedRenderer.SqlId = "Test Sql 2";
                expectedRenderer.SqlResColumn = "Test Res Column 2";

                await mgr.PutPdfAnnotationRenderer(expectedRenderer);

                actualRenderer = await mgr.Get(rendererBaseId);
                Assert.IsNotNull(actualRenderer);
                AssertHelper.AssertObject(expectedRenderer, actualRenderer);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(typeof(PdfAnnotationRendererEFCoreManager))]
        public async Task TestPdfBarcodeRendererManager_Post(Type managerType)
        {
            try
            {
                var mgr = (IPdfAnnotationRendererManager)Activator.CreateInstance(managerType);

                var rendererBaseId = Guid.NewGuid();

                var expectedRenderer = new PdfAnnotationRendererModel
                {
                    RendererBase = new PdfRendererBaseModel
                    {
                        PdfRendererBaseId = rendererBaseId,
                        Id = "Test Pdf Barcode Renderer 1",
                        RendererType = PdfRendererType.Barcode,
                        Margin = null,
                        Padding = null,
                        HorizontalAlignment = null,
                        VerticalAlignment = null,
                        Position = null,
                        Left = null,
                        Right = null,
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
                    },
                    AnnotationRendererType = AnnotationRendererType.Text,
                    Title = "Test Title 1",
                    Icon = PdfTextAnnotationIcon.Paragraph,
                    SqlTemplateId = "Test Sql Template 1",
                    SqlId = "Test Sql 1",
                    SqlResColumn = "Test Res Column 1"
                };

                await mgr.Post(expectedRenderer);

                var actualRenderer = await mgr.Get(rendererBaseId);
                Assert.IsNotNull(actualRenderer);
                AssertHelper.AssertObject(expectedRenderer, actualRenderer);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}