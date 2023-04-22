using System;
using System.Threading.Tasks;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfBarcodeRenderer;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using ZXing;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfBarcodeRendererManagerTest : PdfRendererManagerTestBase<PdfBarcodeRendererModel>
    {
        [Test]
        [TestCase(typeof(PdfBarcodeRendererEFCoreManager), true)]
        [TestCase(typeof(PdfBarcodeRendererEFCoreManager), false)]
        [TestCase(typeof(PdfBarcodeRendererSPManager), true)]
        [TestCase(typeof(PdfBarcodeRendererSPManager), false)]
        public async Task TestPdfBarcodeRendererManager_Get(Type managerType, bool createNull)
        {
            try
            {
                var mgr = (PdfRendererManagerBase<PdfBarcodeRendererModel>)Activator.CreateInstance(managerType);

                var rendererBaseId = Guid.NewGuid();
                var rendererType = PdfRendererType.Barcode;

                var expectedRenderer = CreatePdfRendererBaseModel(rendererBaseId, rendererType, !createNull);

                expectedRenderer.BarcodeFormat = createNull ? null : (BarcodeFormat?)BarcodeFormat.PHARMA_CODE;
                expectedRenderer.ShowBarcodeText = true;
                expectedRenderer.SqlTemplateId = createNull ? null : "Test Sql Template 1";
                expectedRenderer.SqlId = createNull ? null : "Test Sql 1";
                expectedRenderer.SqlResColumn = createNull ? null : "Test Res Column 1";

                await mgr.Post(expectedRenderer);

                var actualRenderer = await mgr.Get(rendererBaseId);
                Assert.IsNotNull(actualRenderer);
                AssertHelper.AssertObject(expectedRenderer, actualRenderer);

                expectedRenderer = CreatePdfRendererBaseModel(rendererBaseId, rendererType, createNull);

                expectedRenderer.BarcodeFormat = createNull ? null : (BarcodeFormat?)BarcodeFormat.UPC_EAN_EXTENSION;
                expectedRenderer.ShowBarcodeText = false;
                expectedRenderer.SqlTemplateId = createNull ? null : "Test Sql Template 2";
                expectedRenderer.SqlId = createNull ? null : "Test Sql 2";
                expectedRenderer.SqlResColumn = createNull ? null : "Test Res Column 2";

                await mgr.Put(expectedRenderer);

                actualRenderer = await mgr.Get(rendererBaseId);
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

