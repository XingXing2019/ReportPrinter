using System.Collections.Generic;

namespace RaphaelLibrary.Code.Render.PDF.Structure
{
    public class PdfReportFooter : PdfStructureBase
    {
        public PdfReportFooter(HashSet<string> rendererNames) 
            : base(PdfStructure.PdfReportFooter, rendererNames) { }
    }
}