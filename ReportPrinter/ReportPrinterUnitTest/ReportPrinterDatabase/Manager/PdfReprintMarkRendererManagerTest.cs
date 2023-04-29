using System;
using System.Threading.Tasks;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfReprintMarkRenderer;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfReprintMarkRendererManagerTest : PdfRendererManagerTestBase<PdfReprintMarkRendererModel>
    {
        [Test]
        [TestCase(typeof(PdfReprintMarkRendererEFCoreManager), true)]
        [TestCase(typeof(PdfReprintMarkRendererEFCoreManager), false)]
        [TestCase(typeof(PdfReprintMarkRendererSPManager), true)]
        [TestCase(typeof(PdfReprintMarkRendererSPManager), false)]
        public async Task TestPdfReprintMarkRendererManager_Get(Type managerType, bool createNull)
        {
            await DoTest(managerType, PdfRendererType.ReprintMark, createNull);
        }

        protected override void AssignPostProperties(PdfReprintMarkRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.Text = "Test reprint mark 1";
            expectedRenderer.BoardThickness = createNull ? null : (double?)2.1;
            expectedRenderer.Location = createNull ? null : (Location?)Location.Footer;
        }

        protected override void AssignPutProperties(PdfReprintMarkRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.Text = "Test reprint mark 2";
            expectedRenderer.BoardThickness = createNull ? null : (double?)3.6;
            expectedRenderer.Location = createNull ? null : (Location?)Location.Body;
        }
    }
}