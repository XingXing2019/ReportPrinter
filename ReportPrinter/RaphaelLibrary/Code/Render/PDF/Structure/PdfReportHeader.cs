using System.Collections.Generic;

namespace RaphaelLibrary.Code.Render.PDF.Structure
{
    public class PdfReportHeader : PdfStructureBase
    {
        public PdfReportHeader(HashSet<string> rendererNames) 
            : base(PdfStructure.PdfReportHeader, rendererNames) { }
    }
}