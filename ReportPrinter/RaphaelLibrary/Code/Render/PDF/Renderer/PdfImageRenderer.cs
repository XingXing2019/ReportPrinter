using System;
using System.IO;
using System.Net;
using System.Xml;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Structure;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfImageRenderer : PdfRendererBase
    {
        private SourceType _sourceType;
        private XImage _image;
        private string _imageSource;
        
        public PdfImageRenderer(PdfStructure position) : base(position) { }

        public override bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            if (!base.ReadXml(node))
            {
                return false;
            }

            var sourceTypeStr = node.SelectSingleNode(XmlElementHelper.S_SOURCE_TYPE)?.InnerText;
            if (!Enum.TryParse(sourceTypeStr, out SourceType sourceType))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_SOURCE_TYPE, node, procName);
                return false;
            }
            _sourceType = sourceType;

            var imageSource = node.SelectSingleNode(XmlElementHelper.S_IMAGE_SOURCE)?.InnerText;
            if (string.IsNullOrEmpty(imageSource))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_IMAGE_SOURCE, node, procName);
                return false;
            }

            if (_sourceType == SourceType.Local)
            {
                if (!XImage.ExistsFile(imageSource))
                {
                    Logger.Error($"Local image source: {imageSource} does not exist or in invalid format", procName);
                    return false;
                }

                _image = XImage.FromFile(imageSource);
            }
            else if (_sourceType == SourceType.Online)
            {
                try
                {
                    using var client = new WebClient();
                    var data = client.DownloadData(new Uri(imageSource));
                    using var stream = new MemoryStream(data);
                    _image = XImage.FromStream(stream);
                }
                catch (Exception)
                {
                    Logger.Error($"Inline image source: {imageSource} does not exist", procName);
                    return false;
                }
            }
            _imageSource = imageSource;

            Logger.Info($"Success to read Image with source type: {_sourceType}, image source: {_imageSource}", procName);
            return true;
        }

        protected override bool TryPerformRender(PdfDocumentManager manager, XGraphics graph, PdfPage page, string procName)
        {
            if (_image == null)
            {
                Logger.Error($"Unable to generate image from source: {_imageSource}", procName);
                return false;
            }
            
            RenderImage(graph);
            return true;
        }


        #region Helper

        private void RenderImage(XGraphics graph)
        {
            var ratio = _image.PointWidth / _image.PointHeight;
            double x = ContentBox.X, y = ContentBox.Y;
            double width = ContentBox.Width, height = ContentBox.Height;

            if (width / height > ratio)
                width = height * ratio;
            else
                height = width / ratio;

            if (HorizontalAlignment == HorizontalAlignment.Center)
                x += ContentBox.Width / 2 - width / 2;
            else if (HorizontalAlignment == HorizontalAlignment.Right)
                x += ContentBox.Width - width;

            if (VerticalAlignment == VerticalAlignment.Center)
                y += ContentBox.Height / 2 - height / 2;
            else if (VerticalAlignment == VerticalAlignment.Bottom)
                y += ContentBox.Height - height;

            var rect = new XRect(x, y, width, height);
            graph.DrawImage(_image, rect);
        }

        #endregion
    }


    public enum SourceType
    {
        Local,
        Online
    }
}