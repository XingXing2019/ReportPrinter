using System;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Structure;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfRendererFactory
    {
        public static PdfRendererBase CreatePdfRenderer(string name, PdfStructure position)
        {
            var procName = $"PdfRendererFactory.{nameof(CreatePdfRenderer)}";

            if (name == XmlElementHelper.S_TEXT)
                return new PdfTextRenderer(position);
            else if (name == XmlElementHelper.S_BARCODE)
                return new PdfBarcodeRenderer(position);
            else
            {
                var error = $"Invalid name: {name} for pdf renderer";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}