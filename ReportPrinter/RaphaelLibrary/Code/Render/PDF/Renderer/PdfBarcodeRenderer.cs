using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.PDF.Structure;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfBarcodeRenderer : PdfRendererBase
    {
        private Sql _sql;
        private SqlResColumn _sqlResColumn;
        private BarcodeFormat _barcodeFormat;
        private bool _showBarcodeText;

        public PdfBarcodeRenderer(PdfStructure location) : base(location) { }

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

            if (!TryReadSql(node, procName, out var sql, out var sqlResColumnList))
                return false;

            if (sqlResColumnList.Count != 1)
            {
                Logger.Error($"{this.GetType().Name} can only have one sql result column", procName);
                return false;
            }

            _sql = sql;
            _sqlResColumn = sqlResColumnList[0];
            Logger.Info($"Success to read {this.GetType().Name} with format: {_barcodeFormat}, sql id: {_sql.Id}, res column: {_sqlResColumn}", procName);
            
            return true;
        }

        public override PdfRendererBase Clone()
        {
            var cloned = base.Clone() as PdfBarcodeRenderer;
            cloned._sql = this._sql.Clone() as Sql;
            cloned._sqlResColumn = this._sqlResColumn.Clone();
            return cloned;
        }

        protected override bool TryPerformRender(PdfDocumentManager manager, string procName)
        {
            var pdf = manager.Pdf;
            var page = pdf.Pages[manager.CurrentPage];
            using var graph = XGraphics.FromPdfPage(page);
            RenderBoxModel(graph);

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