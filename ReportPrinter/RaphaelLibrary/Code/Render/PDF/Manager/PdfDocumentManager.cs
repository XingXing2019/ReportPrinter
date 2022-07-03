using System;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace RaphaelLibrary.Code.Render.PDF.Manager
{
    public class PdfDocumentManager
    {
        public PdfDocument Pdf { get; }
        public Guid MessageId { get; }

        private readonly XSize _pageSize;
        
        public PdfDocumentManager(Guid messageId, PdfDocument pdf, XSize pageSize)
        {
            _pageSize = pageSize;
            MessageId = messageId;
            Pdf = pdf;
        }
        

        public PdfPage AddPage()
        {
            var page = Pdf.AddPage();
            page.Height = _pageSize.Height;
            page.Width = _pageSize.Width;
            return page;
        }
    }
}