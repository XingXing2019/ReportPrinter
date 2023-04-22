using NUnit.Framework;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfImageRenderer;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using System.Threading.Tasks;
using System;
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
            try
            {
                var mgr = (PdfRendererManagerBase<PdfPageNumberRendererModel>)Activator.CreateInstance(managerType);

                var rendererBaseId = Guid.NewGuid();
                var rendererType = PdfRendererType.PageNumber;

                var expectedRenderer = CreatePdfRendererBaseModel(rendererBaseId, rendererType, !createNull);

                expectedRenderer.StartPage = createNull ? null : (int?)1;
                expectedRenderer.EndPage = createNull ? null : (int?)2;
                expectedRenderer.PageNumberLocation = createNull ? null : (Location?)Location.Footer;

                await mgr.Post(expectedRenderer);

                var actualRenderer = await mgr.Get(rendererBaseId);
                Assert.IsNotNull(actualRenderer);

                AssertHelper.AssertObject(expectedRenderer, actualRenderer);

                expectedRenderer = CreatePdfRendererBaseModel(rendererBaseId, rendererType, createNull);

                expectedRenderer.StartPage = createNull ? null : (int?)5;
                expectedRenderer.EndPage = createNull ? null : (int?)-2;
                expectedRenderer.PageNumberLocation = createNull ? null : (Location?)Location.Body;

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