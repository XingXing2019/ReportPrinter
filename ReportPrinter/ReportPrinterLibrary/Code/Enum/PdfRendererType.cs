using System.ComponentModel;

namespace ReportPrinterLibrary.Code.Enum
{
    public enum PdfRendererType : byte
    {
        [Description("Annotation")]
        Annotation = 0,

        [Description("Barcode")]
        Barcode = 1,

        [Description("Image")]
        Image = 2,

        [Description("Page Number")]
        PageNumber = 3,

        [Description("Reprint Mark")]
        ReprintMark = 4,

        [Description("Table")]
        Table = 5,

        [Description("Text")]
        Text = 6,

        [Description("Watermark")]
        Watermark = 7,
    }
}