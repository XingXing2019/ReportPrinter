using NUnit.Framework;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using System.Threading.Tasks;
using System;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfImageRenderer;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfImageRendererManagerTest : PdfRendererManagerTestBase<PdfImageRendererModel>
    {
        [Test]
        [TestCase(typeof(PdfImageRendererEFCoreManager), true)]
        [TestCase(typeof(PdfImageRendererEFCoreManager), false)]
        [TestCase(typeof(PdfImageRendererSPManager), true)]
        [TestCase(typeof(PdfImageRendererSPManager), false)]
        public async Task TesPdfImageRendererEFCoreManager_Get(Type managerType, bool createNull)
        {
            try
            {
                var mgr = (PdfRendererManagerBase<PdfImageRendererModel>)Activator.CreateInstance(managerType);

                var rendererBaseId = Guid.NewGuid();
                var rendererType = PdfRendererType.Image;

                var expectedRenderer = CreatePdfRendererBaseModel(rendererBaseId, rendererType, !createNull);

                expectedRenderer.SourceType = SourceType.Local;
                expectedRenderer.ImageSource = "Test Image Source 1";

                await mgr.Post(expectedRenderer);

                var actualRenderer = await mgr.Get(rendererBaseId);
                Assert.IsNotNull(actualRenderer);

                AssertHelper.AssertObject(expectedRenderer, actualRenderer);

                expectedRenderer = CreatePdfRendererBaseModel(rendererBaseId, rendererType, createNull);

                expectedRenderer.SourceType = SourceType.Online;
                expectedRenderer.ImageSource = "Test Image Source 2";

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