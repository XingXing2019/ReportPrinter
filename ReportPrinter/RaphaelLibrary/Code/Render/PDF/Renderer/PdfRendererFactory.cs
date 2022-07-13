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
            else if (name == XmlElementHelper.S_IMAGE)
                return new PdfImageRenderer(position);
            else if (name == XmlElementHelper.S_ANNOTATION)
                return new PdfAnnotationRenderer(position);
            else if (name == XmlElementHelper.S_TABLE)
                return new PdfTableRenderer(position);
            else if (name == XmlElementHelper.S_WATER_MARK)
                return new PdfWaterMarkRenderer(position);
            else if (name == XmlElementHelper.S_PAGE_NUMBER)
                return new PdfPageNumberRenderer(position);
            else if (name == XmlElementHelper.S_REPRINT_MARK)
                return new PdfReprintMarkRenderer(position);
            else
            {
                var error = $"Invalid name: {name} for pdf renderer";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}