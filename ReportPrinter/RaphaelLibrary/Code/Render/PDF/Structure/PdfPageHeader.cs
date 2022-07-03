using System.Collections.Generic;

namespace RaphaelLibrary.Code.Render.PDF.Structure
{
    public class PdfPageHeader : PdfStructureBase
    {
        public PdfPageHeader(HashSet<string> rendererNames) 
            : base(PdfStructure.PdfPageHeader, rendererNames) { }
    }
}