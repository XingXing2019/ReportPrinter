namespace RaphaelLibrary.Code.Render.PDF.Model
{
    public class ContainerModel
    {
        public double LeftBoundary { get; set; }
        public double RightBoundary { get; set; }
        public double FirstPageTopBoundary { get; set; }
        public double NonFirstPageTopBoundary { get; set; }
        public double LastPageBottomBoundary { get; set; }
        public double NonLastPageBottomBoundary { get; set; }
    }
}