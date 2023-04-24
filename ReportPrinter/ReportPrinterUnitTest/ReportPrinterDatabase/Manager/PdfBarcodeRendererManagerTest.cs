using System;
using System.Threading.Tasks;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfBarcodeRenderer;
using ReportPrinterDatabase.Code.Model;
using ZXing;
using BarcodeFormat = ReportPrinterLibrary.Code.Enum.BarcodeFormat;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfBarcodeRendererManagerTest : PdfRendererManagerTestBase<PdfBarcodeRendererModel, PdfBarcodeRenderer>
    {
        [Test]
        [TestCase(typeof(PdfBarcodeRendererEFCoreManager), true)]
        [TestCase(typeof(PdfBarcodeRendererEFCoreManager), false)]
        [TestCase(typeof(PdfBarcodeRendererSPManager), true)]
        [TestCase(typeof(PdfBarcodeRendererSPManager), false)]
        public async Task TestPdfBarcodeRendererManager_Get(Type managerType, bool createNull)
        {
            await DoTest(managerType, createNull);
        }

        protected override void AssignPostProperties(PdfBarcodeRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.BarcodeFormat = createNull ? null : (BarcodeFormat?)BarcodeFormat.PHARMA_CODE;
            expectedRenderer.ShowBarcodeText = true;
            expectedRenderer.SqlTemplateId = "Test Sql Template 1";
            expectedRenderer.SqlId = "Test Sql 1";
            expectedRenderer.SqlResColumn = "Test Res Column 1";
        }

        protected override void AssignPutProperties(PdfBarcodeRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.BarcodeFormat = createNull ? null : (BarcodeFormat?)BarcodeFormat.UPC_EAN_EXTENSION;
            expectedRenderer.ShowBarcodeText = false;
            expectedRenderer.SqlTemplateId = "Test Sql Template 2";
            expectedRenderer.SqlId = "Test Sql 2";
            expectedRenderer.SqlResColumn = "Test Res Column 2";
        }
    }
}

