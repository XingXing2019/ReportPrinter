namespace RaphaelLibrary.Code.Render.PDF.Model
{
    public class TextSize
    {
        public double Width { get; }
        public double Height { get; }
        public double WidthPerLetter { get; }

        public TextSize(double width, double height, double widthPerLetter)
        {
            Width = width;
            Height = height;
            WidthPerLetter = widthPerLetter;
        }
    }
}