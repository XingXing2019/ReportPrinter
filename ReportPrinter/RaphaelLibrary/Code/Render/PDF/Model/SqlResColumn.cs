using System;
using System.Xml;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Renderer;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Model
{
    public class SqlResColumn : IXmlReader
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public double WidthRatio { get; set; }
        public Position Position { get; set; }
        public double Left { get; set; }
        public double Right { get; set; }

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

            var positionStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_POSITION);
            if (!Enum.TryParse(positionStr, out Position position))
            {
                position = Position.Static;
                Logger.LogDefaultValue(node, XmlElementHelper.S_POSITION, position, procName);
            }
            Position = position;

            var leftStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_LEFT);
            if (!double.TryParse(leftStr, out var left))
            {
                left = 0;
                Logger.LogDefaultValue(node, XmlElementHelper.S_LEFT, left, procName);
            }
            Left = left;

            var rightStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_RIGHT);
            if (!double.TryParse(rightStr, out var right))
            {
                right = 0;
                Logger.LogDefaultValue(node, XmlElementHelper.S_RIGHT, right, procName);
            }
            Right = right;

            if (!string.IsNullOrEmpty(leftStr) && !string.IsNullOrEmpty(rightStr))
            {
                Logger.Error($"Cannot have left and right at same time", procName);
                return false;
            }

            Logger.Info($"Success to read SqlResColumn: {Id}, title: {Title}, width ratio: {WidthRatio}", procName);
            return true;
        }

        public SqlResColumn Clone()
        {
            return this.MemberwiseClone() as SqlResColumn;
        }
    }
}