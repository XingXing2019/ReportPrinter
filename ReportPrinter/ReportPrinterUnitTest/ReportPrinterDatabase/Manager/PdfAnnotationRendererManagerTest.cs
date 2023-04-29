using NUnit.Framework;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using System.Threading.Tasks;
using System;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfAnnotationRenderer;
using PdfTextAnnotationIcon = ReportPrinterLibrary.Code.Enum.PdfTextAnnotationIcon;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfAnnotationRendererManagerTest : PdfRendererManagerTestBase<PdfAnnotationRendererModel>
    {
        [Test]
        [TestCase(typeof(PdfAnnotationRendererEFCoreManager), true)]
        [TestCase(typeof(PdfAnnotationRendererEFCoreManager), false)]
        [TestCase(typeof(PdfAnnotationRendererSPManager), true)]
        [TestCase(typeof(PdfAnnotationRendererSPManager), false)]
        public async Task TesPdfAnnotationRendererEFCoreManager_Get(Type managerType, bool createNull)
        {
            await DoTest(managerType, PdfRendererType.Annotation, createNull);
        }

        protected override void AssignPostProperties(PdfAnnotationRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.AnnotationRendererType = AnnotationRendererType.Text;
            expectedRenderer.Title = createNull ? null : "Test Title 1";
            expectedRenderer.Icon = createNull ? null : (PdfTextAnnotationIcon?)PdfTextAnnotationIcon.Insert;
            expectedRenderer.Content = createNull ? null : "Test Content 1";
            expectedRenderer.SqlTemplateConfigSqlConfigId = createNull ? null : (Guid?)sqlInfoId;
            expectedRenderer.SqlTemplateId = createNull ? null : "Test Sql Template 1";
            expectedRenderer.SqlId = createNull ? null : "Test Sql 1";
            expectedRenderer.SqlResColumn = createNull ? null : "Test Res Column 1";
        }

        protected override void AssignPutProperties(PdfAnnotationRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.AnnotationRendererType = AnnotationRendererType.Sql;
            expectedRenderer.Title = createNull ? null : "Test Title 2";
            expectedRenderer.Icon = createNull ? null : (PdfTextAnnotationIcon?)PdfTextAnnotationIcon.Insert;
            expectedRenderer.Content = createNull ? null : "Test Content 2";
            expectedRenderer.SqlTemplateConfigSqlConfigId = createNull ? null : (Guid?)sqlInfoId;
            expectedRenderer.SqlTemplateId = createNull ? null : "Test Sql Template 2";
            expectedRenderer.SqlId = createNull ? null : "Test Sql 2";
            expectedRenderer.SqlResColumn = createNull ? null : "Test Res Column 2";
        }
    }
}