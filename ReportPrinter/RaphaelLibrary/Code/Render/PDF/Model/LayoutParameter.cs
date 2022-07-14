using RaphaelLibrary.Code.Render.PDF.Renderer;
using RaphaelLibrary.Code.Render.PDF.Structure;

namespace RaphaelLibrary.Code.Render.PDF.Model
{
    public class LayoutParameter
    {
        public MarginPaddingModel Margin { get; set; }
        public MarginPaddingModel Padding { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int RowSpan { get; set; }
        public int ColumnSpan { get; set; }
        public Position Position { get; set; }
        public double Left { get; set; }
        public double Right { get; set; }
        public double Top { get; set; }
        public double Bottom { get; set; }
    }
}