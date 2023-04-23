namespace ReportPrinterLibrary.Code.Enum
{
    public enum XFontStyle : byte
    {
        Regular = 0,
        Bold = 1,
        Italic = 2,
        BoldItalic = Italic | Bold, // 0x00000003
        Underline = 4,
        Strikeout = 8,
    }
}