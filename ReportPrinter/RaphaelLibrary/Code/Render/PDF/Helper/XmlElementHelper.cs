using System.Xml;

namespace RaphaelLibrary.Code.Render.PDF.Helper
{
    public class XmlElementHelper
    {
        #region XmlNode

        public const string S_ANNOTATION = "Annotation";
        public const string S_BARCODE = "Barcode";
        public const string S_BARCODE_FORMAT = "BarcodeFormat";
        public const string S_COLUMN = "Column";
        public const string S_COLUMN_SPAN = "ColumnSpan";
        public const string S_CONTENT = "Content";
        public const string S_DATABASE_ID = "DatabaseId";
        public const string S_ICON = "Icon";
        public const string S_IMAGE = "Image";
        public const string S_IMAGE_SOURCE = "ImageSource";
        public const string S_Mask = "Mask";
        public const string S_PAGE_BODY = "PageBody";
        public const string S_PAGE_FOOTER = "PageFooter";
        public const string S_PAGE_HEADER = "PageHeader";
        public const string S_PDF_TEMPLATE = "PdfTemplate";
        public const string S_PDF_TEMPLATE_LIST = "PdfTemplateList";
        public const string S_QUERY = "Query";
        public const string S_ReportFooter = "ReportFooter";
        public const string S_ReportHeader = "ReportHeader";
        public const string S_ROW = "Row";
        public const string S_ROW_SPAN = "RowSpan";
        public const string S_SHOW_BARCODE_TEXT = "ShowBarcodeText";
        public const string S_SOURCE_TYPE = "SourceType";
        public const string S_SQL = "Sql";
        public const string S_SQL_ID = "SqlId";
        public const string S_SQL_RES_COLUMN = "SqlResColumn";
        public const string S_SQL_TEMPLATE = "SqlTemplate";
        public const string S_SQL_TEMPLATE_ID = "SqlTemplateId";
        public const string S_SQL_TEMPLATE_LIST = "SqlTemplateList";
        public const string S_TEXT = "Text";
        public const string S_TITLE = "Title";
        public const string S_TYPE = "Type";
        public const string S_VARIABLE = "Variable";

        #endregion


        #region Attribute

        public const string S_BACKGROUND_COLOR = "BackgroundColor";
        public const string S_BRUSH_COLOR = "BrushColor";
        public const string S_COLUMNS = "Columns";
        public const string S_FILE_NAME_SUFFIX = "FileNameSuffix";
        public const string S_FONT_FAMILY = "FontFamily";
        public const string S_FONT_SIZE = "FontSize";
        public const string S_FONT_STYLE = "FontStyle";
        public const string S_HEIGHT = "Height";
        public const string S_HORIZONTAL_ALIGNMENT = "HorizontalAlignment";
        public const string S_ID = "Id";
        public const string S_MARGIN = "Margin";
        public const string S_NAME = "Name";
        public const string S_OPACITY = "Opacity";
        public const string S_ORIENTATION = "Orientation";
        public const string S_PADDING = "Padding";
        public const string S_PAGE_SIZE = "PageSize";
        public const string S_ROWS = "Rows";
        public const string S_SAVE_PATH = "SavePath";
        public const string S_VERTICAL_ALIGNMENT = "VerticalAlignment";

        #endregion

        public static string GetAttribute(XmlNode node, string name)
        {
            return node.Attributes?[name]?.Value;
        }
    }
}