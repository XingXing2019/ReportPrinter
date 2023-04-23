using PdfSharp.Drawing;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using NUnit.Framework;
using XFontStyle = ReportPrinterLibrary.Code.Enum.XFontStyle;
using XKnownColor = ReportPrinterLibrary.Code.Enum.XKnownColor;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public abstract class PdfRendererManagerTestBase<T, E> : DatabaseTestBase<PdfRendererBaseModel> where T : PdfRendererBaseModel
    {
        protected PdfRendererManagerTestBase()
        {
            Manager = new PdfRendererBaseEFCoreManager();
        }
        
        [TearDown]
        public new void TearDown()
        {
            Manager.DeleteAll().Wait();
        }

        protected async Task DoTest(Type managerType, bool createNull)
        {
            try
            {
                var mgr = (PdfRendererManagerBase<T, E>)Activator.CreateInstance(managerType);

                var rendererBaseId = Guid.NewGuid();
                var rendererType = PdfRendererType.Barcode;

                var expectedRenderer = CreatePdfRendererBaseModel(rendererBaseId, rendererType, !createNull);

                AssignPostProperties(expectedRenderer, createNull);

                await mgr.Post(expectedRenderer);

                var actualRenderer = await mgr.Get(rendererBaseId);
                Assert.IsNotNull(actualRenderer);
                AssertHelper.AssertObject(expectedRenderer, actualRenderer);

                expectedRenderer = CreatePdfRendererBaseModel(rendererBaseId, rendererType, createNull);

                AssignPutProperties(expectedRenderer, createNull);

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

        protected abstract void AssignPostProperties(T expectedRenderer, bool createNull);
        protected abstract void AssignPutProperties(T expectedRenderer, bool createNull);


        private T CreatePdfRendererBaseModel(Guid rendererBaseId, PdfRendererType rendererType, bool createNull)
        {
            var renderer = Activator.CreateInstance<T>();

            renderer.PdfRendererBaseId = rendererBaseId;
            renderer.Id = "Test Pdf Renderer 1";
            renderer.RendererType = rendererType;
            renderer.Margin = createNull ? null : "10, 5, 10, 5";
            renderer.Padding = createNull ? null : "5, 10, 5, 10";
            renderer.HorizontalAlignment = createNull ? null : (HorizontalAlignment?)HorizontalAlignment.Left;
            renderer.VerticalAlignment = createNull ? null : (VerticalAlignment?)VerticalAlignment.Center;
            renderer.Position = createNull ? null :  (Position?)Position.Static;
            renderer.Left = createNull ? null : (double?) 10.5;
            renderer.Right = createNull ? null : (double?)5.5;
            renderer.Top = createNull ? null : (double?)20.5;
            renderer.Bottom = createNull ? null : (double?)15.5;
            renderer.FontSize = createNull ? null : (double?)6.5;
            renderer.FontFamily = createNull ? null : "Time New Roman";
            renderer.FontStyle = createNull ? null : (XFontStyle?)XFontStyle.Strikeout;
            renderer.Opacity = createNull ? null : (double?)0.9;
            renderer.BrushColor = createNull ? null : (XKnownColor?)XKnownColor.YellowGreen;
            renderer.BackgroundColor = createNull ? null : (XKnownColor?)XKnownColor.Brown;
            renderer.Row = 2;
            renderer.Column = 4;
            renderer.RowSpan = createNull ? null : (int?)5;
            renderer.ColumnSpan = createNull ? null : (int?) 8;
            
            return renderer;
        }
    }
}