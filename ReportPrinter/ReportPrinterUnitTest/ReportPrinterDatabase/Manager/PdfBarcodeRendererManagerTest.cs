using System;
using System.Threading.Tasks;
using NUnit.Framework;
using PdfSharp.Drawing;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfBarcodeRenderer;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using ZXing;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfBarcodeRendererManagerTest : DatabaseTestBase<PdfRendererBaseModel>
    {
        public PdfBarcodeRendererManagerTest()
        {
            Manager = new PdfRendererBaseEFCoreManager();
        }

        [TearDown]
        public new void TearDown()
        {
            Manager.DeleteAll().Wait();
        }

        [Test]
        [TestCase(typeof(PdfBarcodeRendererEFCoreManager))]
        [TestCase(typeof(PdfBarcodeRendererSPManager))]
        public async Task TestPdfBarcodeRendererManager_Get(Type managerType)
        {
            try
            {
                var mgr = (IPdfBarcodeRendererManager)Activator.CreateInstance(managerType);

                var rendererBaseId = Guid.NewGuid();

                var expectedRenderer = new PdfBarcodeRendererModel
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
                    BarcodeFormat = BarcodeFormat.PHARMA_CODE,
                    ShowBarcodeText = true,
                    SqlTemplateId = "Test Sql Template 1",
                    SqlId = "Test Sql 1",
                    SqlResColumn = "Test Res Column 1"
                };

                await mgr.Post(expectedRenderer);

                var actualRenderer = await mgr.Get(rendererBaseId);
                Assert.IsNotNull(actualRenderer);
                AssertHelper.AssertObject(expectedRenderer, actualRenderer);

                expectedRenderer.Id = "Test Pdf Barcode Renderer 2";
                expectedRenderer.RendererType = PdfRendererType.Watermark;
                expectedRenderer.Margin = null;
                expectedRenderer.Padding = null;
                expectedRenderer.HorizontalAlignment = null;
                expectedRenderer.VerticalAlignment = null;
                expectedRenderer.Position = null;
                expectedRenderer.Left = null;
                expectedRenderer.Right = null;
                expectedRenderer.Top = null;
                expectedRenderer.Bottom = null;
                expectedRenderer.FontSize = null;
                expectedRenderer.FontFamily = null;
                expectedRenderer.FontStyle = null;
                expectedRenderer.Opacity = null;
                expectedRenderer.BrushColor = null;
                expectedRenderer.BackgroundColor = null;
                expectedRenderer.Row = 5;
                expectedRenderer.Column = 1;
                expectedRenderer.RowSpan = null;
                expectedRenderer.ColumnSpan = null;
                expectedRenderer.BarcodeFormat = null;
                expectedRenderer.ShowBarcodeText = false;
                expectedRenderer.SqlTemplateId = "Test Sql Template 2";
                expectedRenderer.SqlId = "Test Sql 2";
                expectedRenderer.SqlResColumn = "Test Res Column 2";

                await mgr.PutPdfBarcodeRenderer(expectedRenderer);

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
        [TestCase(typeof(PdfBarcodeRendererEFCoreManager))]
        [TestCase(typeof(PdfBarcodeRendererSPManager))]
        public async Task TestPdfBarcodeRendererManager_Post(Type managerType)
        {
            try
            {
                var mgr = (IPdfBarcodeRendererManager)Activator.CreateInstance(managerType);

                var rendererBaseId = Guid.NewGuid();

                var expectedRenderer = new PdfBarcodeRendererModel
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
                    BarcodeFormat = null,
                    ShowBarcodeText = false,
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

