using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Structure;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterLibrary.Code.Log;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfBarcodeRenderer : PdfRendererBase
    {
        private Sql _sql;
        private string _sqlResColumn;
        private BarcodeFormat _barcodeFormat;
        private bool _showBarcodeText;

        public PdfBarcodeRenderer(PdfStructure position) : base(position) { }

        public override bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            if (!base.ReadXml(node))
            {
                return false;
            }

            var barcodeFormatStr = node.SelectSingleNode(XmlElementHelper.S_BARCODE_FORMAT)?.InnerText;
            if (string.IsNullOrEmpty(barcodeFormatStr))
            {
                barcodeFormatStr = "CODE_128";
                Logger.LogDefaultValue(node, XmlElementHelper.S_BARCODE_FORMAT, barcodeFormatStr, procName);
            }
            _barcodeFormat = Enum.TryParse(barcodeFormatStr, out BarcodeFormat barcodeFormat) ? barcodeFormat : BarcodeFormat.CODE_128;

            var showBarcodeTextStr = node.SelectSingleNode(XmlElementHelper.S_SHOW_BARCODE_TEXT)?.InnerText;
            if (!bool.TryParse(showBarcodeTextStr, out var showBarcodeText))
            {
                showBarcodeText = false;
                Logger.LogDefaultValue(node, XmlElementHelper.S_SHOW_BARCODE_TEXT, showBarcodeText, procName);
            }
            _showBarcodeText = showBarcodeText;

            if (!TryReadSql(node, procName, out var sql, out var sqlResColumn))
                return false;

            _sql = sql;
            _sqlResColumn = sqlResColumn;
            Logger.Info($"Success to read Barcode with format: {_barcodeFormat}, sql id: {_sql.Id}, res column: {_sqlResColumn}", procName);
            
            return true;
        }

        public override PdfRendererBase Clone()
        {
            var cloned = base.Clone() as PdfBarcodeRenderer;
            cloned._sql = this._sql.Clone() as Sql;
            return cloned;
        }

        protected override bool TryPerformRender(PdfDocumentManager manager, XGraphics graph, string procName)
        {
            if (!_sql.TryExecute(manager.MessageId, _sqlResColumn, out var res))
                return false;

            RenderBarcode(graph, res);
            return true;
        }


        #region Helper

        private void RenderBarcode(XGraphics graph, string barcodeText)
        {
            if (!Enum.TryParse(Font.Style.ToString(), out FontStyle fontStyle))
                fontStyle = FontStyle.Regular;
            var font = new Font(Font.Name, (float)Font.Size, fontStyle);

            var brushColor = BrushColor.Color;
            var color = Color.FromArgb((int)(Opacity * byte.MaxValue), brushColor.R, brushColor.G, brushColor.B);

            var writer = new BarcodeWriter
            {
                Format = _barcodeFormat,
                Renderer = new BitmapRenderer { TextFont = font, Foreground = color}
            };

            if (!_showBarcodeText)
            {
                writer.Options = new EncodingOptions { PureBarcode = true };
            }

            var barcode = writer.Write(barcodeText);
            var stream = new MemoryStream();
            barcode.Save(stream, ImageFormat.Png);
            var image = XImage.FromStream(stream);

            var rect = new XRect(ContentBox.X, ContentBox.Y, ContentBox.Width, ContentBox.Height);
            graph.DrawImage(image, rect);
        }

        #endregion
    }
}