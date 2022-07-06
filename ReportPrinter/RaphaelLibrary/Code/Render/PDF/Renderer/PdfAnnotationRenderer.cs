using System;
using System.Xml;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Structure;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfAnnotationRenderer : PdfRendererBase
    {
        private AnnotationRendererType _annotationRendererType;
        private string _content;

        private Sql _sql;
        private string _sqlResColumn;

        public PdfAnnotationRenderer(PdfStructure position) : base(position)
        {
        }

        public override bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            if (!base.ReadXml(node))
            {
                return false;
            }

            var typeStr = node.SelectSingleNode(XmlElementHelper.S_TYPE)?.InnerText;
            if (!Enum.TryParse(typeStr, out AnnotationRendererType annotationRendererType))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_TYPE, node, procName);
                return false;
            }
            _annotationRendererType = annotationRendererType;

            if (_annotationRendererType == AnnotationRendererType.Text)
            {
                if (!TryReadContent(node, procName, out var content))
                    return false;
                
                _content = content;
                Logger.Info($"Success to read Annotation with type of {_annotationRendererType}, content: {_content}", procName);
            }
            else if (_annotationRendererType == AnnotationRendererType.Sql)
            {
                if (!TryReadSql(node, procName, out var sql, out var sqlResColumn))
                    return false;

                _sql = sql;
                _sqlResColumn = sqlResColumn;
                Logger.Info($"Success to read Annotation with type of {_annotationRendererType}, sql id: {_sql.Id}, res column: {_sqlResColumn}", procName);
            }

            return true;
        }

        public override PdfRendererBase Clone()
        {
            var cloned = base.Clone() as PdfAnnotationRenderer;
            if (_annotationRendererType == AnnotationRendererType.Sql)
            {
                cloned._sql = this._sql.Clone() as Sql;
            }
            return cloned;
        }

        protected override bool TryPerformRender(PdfDocumentManager manager, XGraphics graph, string procName)
        {
            throw new System.NotImplementedException();

        }
    }

    public enum AnnotationRendererType
    {
        Text,
        Sql
    }
}