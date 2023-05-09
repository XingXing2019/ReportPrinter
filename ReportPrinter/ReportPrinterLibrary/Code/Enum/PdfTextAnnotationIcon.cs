using System.ComponentModel;

namespace ReportPrinterLibrary.Code.Enum
{
    public enum PdfTextAnnotationIcon : byte
    {
        [Description("No Icon")]
        NoIcon,

        [Description("Comment")]
        Comment,

        [Description("Help")]
        Help,

        [Description("Insert")]
        Insert,

        [Description("Key")]
        Key,

        [Description("New Paragraph")]
        NewParagraph,

        [Description("Note")]
        Note,

        [Description("Paragraph")]
        Paragraph,
    }
}