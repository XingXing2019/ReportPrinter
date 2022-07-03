using System;
using RaphaelLibrary.Code.Render.PDF.Renderer;
using RaphaelLibrary.Code.Render.PDF.Structure;

namespace RaphaelLibrary.Code.Render.PDF.Model
{
    public class LayoutParameter
    {
        public MarginPaddingModel Margin { get; set; }
        public MarginPaddingModel Padding { get; set; }
        public PdfStructure Position { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int RowSpan { get; set; }
        public int ColumnSpan { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
    }
}