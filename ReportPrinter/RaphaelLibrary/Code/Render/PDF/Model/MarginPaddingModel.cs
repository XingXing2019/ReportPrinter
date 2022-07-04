namespace RaphaelLibrary.Code.Render.PDF.Model
{
    public class MarginPaddingModel
    {
        public double Top { get; }
        public double Right { get; }
        public double Bottom { get; }
        public double Left { get; }

        public MarginPaddingModel(double top, double right, double bottom, double left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
    }
}