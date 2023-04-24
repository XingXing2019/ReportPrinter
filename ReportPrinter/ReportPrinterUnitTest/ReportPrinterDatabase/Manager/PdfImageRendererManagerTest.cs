using NUnit.Framework;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using System.Threading.Tasks;
using System;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfImageRenderer;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfImageRendererManagerTest : PdfRendererManagerTestBase<PdfImageRendererModel, PdfImageRenderer>
    {
        [Test]
        [TestCase(typeof(PdfImageRendererEFCoreManager), true)]
        [TestCase(typeof(PdfImageRendererEFCoreManager), false)]
        [TestCase(typeof(PdfImageRendererSPManager), true)]
        [TestCase(typeof(PdfImageRendererSPManager), false)]
        public async Task TesPdfImageRendererEFCoreManager_Get(Type managerType, bool createNull)
        {
            await DoTest(managerType, createNull);
        }

        protected override void AssignPostProperties(PdfImageRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.SourceType = SourceType.Local;
            expectedRenderer.ImageSource = "Test Image Source 1";
        }

        protected override void AssignPutProperties(PdfImageRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.SourceType = SourceType.Online;
            expectedRenderer.ImageSource = "Test Image Source 2";
        }
    }
}