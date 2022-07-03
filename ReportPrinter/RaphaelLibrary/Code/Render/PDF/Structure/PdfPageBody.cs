using System.Collections.Generic;

namespace RaphaelLibrary.Code.Render.PDF.Structure
{
    public class PdfPageBody : PdfStructureBase
    {
        public PdfPageBody(HashSet<string> rendererNames) 
            : base(PdfStructure.PdfPageBody, rendererNames) { }
    }
}