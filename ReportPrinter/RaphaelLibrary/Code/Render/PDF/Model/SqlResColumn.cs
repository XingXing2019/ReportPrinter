using System.Xml;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.PDF.Helper;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Model
{
    public class SqlResColumn : IXmlReader
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public double WidthRatio { get; set; }


        public bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            var id = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_ID);
            if (string.IsNullOrEmpty(id))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_ID, node, procName);
                return false;
            }
            Id = id;

            var title = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_TITLE);
            if (string.IsNullOrEmpty(title))
            {
                title = id;
                Logger.LogDefaultValue(node, XmlElementHelper.S_TITLE, title, procName);
            }
            Title = title;

            var widthRatioStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_WIDTH);
            if (!double.TryParse(widthRatioStr?.Substring(0, widthRatioStr.Length - 1), out var widthRatio))
            {
                widthRatio = 1;
                Logger.LogDefaultValue(node, XmlElementHelper.S_WIDTH, widthRatio, procName);
            }
            WidthRatio = widthRatio;

            Logger.Info($"Success to read SqlResColumn: {Id}, title: {Title}, width ratio: {WidthRatio}", procName);
            return true;
        }

        public SqlResColumn Clone()
        {
            return this.MemberwiseClone() as SqlResColumn;
        }
    }
}