using System;
using RaphaelLibrary.Code.Render.PDF.Helper;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfRendererFactory
    {
        public static PdfRendererBase CreatePdfRenderer(string name)
        {
            var procName = $"PdfRendererFactory.{nameof(CreatePdfRenderer)}";

            if (name == XmlElementHelper.S_TEXT)
                return new PdfTextRenderer();

            else
            {
                var error = $"Invalid name: {name} for pdf renderer";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}