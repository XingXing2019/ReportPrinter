using NUnit.Framework;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using System.Threading.Tasks;
using System;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfPageNumberRenderer;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfPageNumberRendererManagerTest : PdfRendererManagerTestBase<PdfPageNumberRendererModel>
    {
        [Test]
        [TestCase(typeof(PdfPageNumberRendererEFCoreManager), true)]
        [TestCase(typeof(PdfPageNumberRendererEFCoreManager), false)]
        [TestCase(typeof(PdfPageNumberRendererSPManager), true)]
        [TestCase(typeof(PdfPageNumberRendererSPManager), false)]
        public async Task TesPdfPageNumberRendererEFCoreManager_Get(Type managerType, bool createNull)
        {
            await DoTest(managerType, PdfRendererType.PageNumber, createNull);
        }

        protected override void AssignPostProperties(PdfPageNumberRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.StartPage = createNull ? null : (int?)1;
            expectedRenderer.EndPage = createNull ? null : (int?)2;
            expectedRenderer.PageNumberLocation = createNull ? null : (Location?)Location.Footer;
        }

        protected override void AssignPutProperties(PdfPageNumberRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.StartPage = createNull ? null : (int?)5;
            expectedRenderer.EndPage = createNull ? null : (int?)-2;
            expectedRenderer.PageNumberLocation = createNull ? null : (Location?)Location.Body;
        }
    }
}