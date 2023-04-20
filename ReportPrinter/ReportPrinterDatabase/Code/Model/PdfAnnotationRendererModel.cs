using System;
using PdfSharp.Pdf.Annotations;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterDatabase.Code.Model
{
    public class PdfAnnotationRendererModel : PdfRendererBaseModel
    {
        public AnnotationRendererType AnnotationRendererType { get; set; }
        public string Title { get; set; }
        public PdfTextAnnotationIcon? Icon { get; set; }
        public string Content { get; set; }
        public string SqlTemplateId { get; set; }
        public string SqlId { get; set; }
        public string SqlResColumn { get; set; }
    }
}