using System;
using System.Threading.Tasks;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfTextRenderer;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using ZXing;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfTextRendererManagerTest : PdfRendererManagerTestBase<PdfTextRendererModel>
    {
        [Test]
        [TestCase(typeof(PdfTextRendererEFCoreManager), true)]
        [TestCase(typeof(PdfTextRendererEFCoreManager), false)]
        [TestCase(typeof(PdfTextRendererSPManager), true)]
        [TestCase(typeof(PdfTextRendererSPManager), false)]
        public async Task TesPdfTextRendererEFCoreManager_Get(Type managerType, bool createNull)
        {
            await DoTest(managerType, createNull);
        }

        protected override void AssignPostProperties(PdfTextRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.TextRendererType = TextRendererType.Sql;
            expectedRenderer.Content = createNull ? null : "Test Content 1";
            expectedRenderer.SqlTemplateConfigSqlConfigId = createNull ? null : (Guid?)sqlInfoId;
            expectedRenderer.SqlTemplateId = createNull ? null : "Test Sql Template 1";
            expectedRenderer.SqlId = createNull ? null : "Test Sql 1";
            expectedRenderer.SqlResColumn = createNull ? null : "Test Res Column 1";
            expectedRenderer.Mask = createNull ? null : "Test Mask 1";
            expectedRenderer.Title = createNull ? null : "Test Title 1";
        }

        protected override void AssignPutProperties(PdfTextRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.TextRendererType = TextRendererType.Timestamp;
            expectedRenderer.Content = createNull ? null : "Test Content 2";
            expectedRenderer.SqlTemplateConfigSqlConfigId = createNull ? null : (Guid?)sqlInfoId;
            expectedRenderer.SqlTemplateId = createNull ? null : "Test Sql Template 2";
            expectedRenderer.SqlId = createNull ? null : "Test Sql 2";
            expectedRenderer.SqlResColumn = createNull ? null : "Test Res Column 2";
            expectedRenderer.Mask = createNull ? null : "Test Mask 2";
            expectedRenderer.Title = createNull ? null : "Test Title 2";
        }
    }
}