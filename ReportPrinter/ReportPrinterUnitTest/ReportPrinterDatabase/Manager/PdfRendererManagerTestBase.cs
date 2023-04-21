using PdfSharp.Drawing;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using System;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterUnitTest.Helper;
using NUnit.Framework;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfRendererManagerTestBase<T> where T : PdfRendererBaseModel
    {
        protected readonly IPdfRendererBaseManager Manager;
        protected readonly AssertHelper AssertHelper;

        public PdfRendererManagerTestBase()
        {
            Manager = new PdfRendererBaseEFCoreManager();
            AssertHelper = new AssertHelper();
        }

        [TearDown]
        public void TearDown()
        {
            Manager.DeleteAll().Wait();
        }

        protected T CreatePdfRendererBaseModel(Guid rendererBaseId, PdfRendererType rendererType, bool createNull)
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