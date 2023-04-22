using System;
using System.Threading.Tasks;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfWaterMarkRenderer;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfWaterMarkRendererManagerTest : PdfRendererManagerTestBase<PdfWaterMarkRendererModel, PdfWaterMarkRenderer>
    {
        [Test]
        [TestCase(typeof(PdfWaterMarkRendererEFCoreManager), true)]
        [TestCase(typeof(PdfWaterMarkRendererEFCoreManager), false)]
        [TestCase(typeof(PdfWaterMarkRendererSPManager), true)]
        [TestCase(typeof(PdfWaterMarkRendererSPManager), false)]
        public async Task TesPdfImageRendererEFCoreManager_Get(Type managerType, bool createNull)
        {
            await DoTest(managerType, createNull);
        }

        protected override void AssignPostProperties(PdfWaterMarkRendererModel expectedRenderer, bool createNull)
        {
            expectedRenderer.WaterMarkType = WaterMarkRendererType.Sql;
            expectedRenderer.Content = createNull ? null : "Test Content 1";
            expectedRenderer.Location = createNull ? null : (Location?)Location.Footer;
            expectedRenderer.SqlTemplateId = createNull ? null : "Test Sql Template 1";
            expectedRenderer.SqlId = createNull ? null : "Test Sql 1";
            expectedRenderer.SqlResColumn = createNull ? null : "Test Res Column 1";
            expectedRenderer.StartPage = createNull ? null : (int?)1;
            expectedRenderer.EndPage = createNull ? null : (int?)-3;
            expectedRenderer.Rotate = createNull ? null : (double?)65.2;
        }

        protected override void AssignPutProperties(PdfWaterMarkRendererModel expectedRenderer, bool createNull)
        {
            expectedRenderer.WaterMarkType = WaterMarkRendererType.Text;
            expectedRenderer.Content = createNull ? null : "Test Content 2";
            expectedRenderer.Location = createNull ? null : (Location?)Location.Footer;
            expectedRenderer.SqlTemplateId = createNull ? null : "Test Sql Template 2";
            expectedRenderer.SqlId = createNull ? null : "Test Sql 2";
            expectedRenderer.SqlResColumn = createNull ? null : "Test Res Column 2";
            expectedRenderer.StartPage = createNull ? null : (int?)4;
            expectedRenderer.EndPage = createNull ? null : (int?)-23;
            expectedRenderer.Rotate = createNull ? null : (double?)45.2;
        }
    }
}