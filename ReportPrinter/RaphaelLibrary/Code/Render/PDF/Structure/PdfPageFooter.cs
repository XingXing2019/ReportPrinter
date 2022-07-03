using System.Collections.Generic;

namespace RaphaelLibrary.Code.Render.PDF.Structure
{
    public class PdfPageFooter : PdfStructureBase
    {
        public PdfPageFooter(HashSet<string> rendererNames)
            : base(PdfStructure.PdfPageFooter, rendererNames) { }
    }
}