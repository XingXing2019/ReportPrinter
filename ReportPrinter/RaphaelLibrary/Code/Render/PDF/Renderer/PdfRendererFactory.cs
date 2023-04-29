using System;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Structure;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfRendererFactory
    {
        public static PdfRendererBase CreatePdfRenderer(string name, PdfStructure location)
        {
            var procName = $"PdfRendererFactory.{nameof(CreatePdfRenderer)}";

            if (name == XmlElementHelper.S_TEXT)
                return new PdfTextRenderer(location);
            else if (name == XmlElementHelper.S_BARCODE)
                return new PdfBarcodeRenderer(location);
            else if (name == XmlElementHelper.S_IMAGE)
                return new PdfImageRenderer(location);
            else if (name == XmlElementHelper.S_ANNOTATION)
                return new PdfAnnotationRenderer(location);
            else if (name == XmlElementHelper.S_TABLE)
                return new PdfTableRenderer(location);
            else if (name == XmlElementHelper.S_WATER_MARK)
                return new PdfWaterMarkRenderer(location);
            else if (name == XmlElementHelper.S_PAGE_NUMBER)
                return new PdfPageNumberRenderer(location);
            else if (name == XmlElementHelper.S_REPRINT_MARK)
                return new PdfReprintMarkRenderer(location);
            else
            {
                var error = $"Invalid name: {name} for pdf renderer";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}