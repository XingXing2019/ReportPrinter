using System;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using RaphaelLibrary.Code.Render.PDF.Model;

namespace RaphaelLibrary.Code.Render.PDF.Manager
{
    public class PdfDocumentManager
    {
        public PdfDocument Pdf { get; }
        public Guid MessageId { get; }
        public ContainerModel PageBodyContainer { get; }

        public double TopBoundary => Pdf.PageCount > 1 ? PageBodyContainer.NonFirstPageTopBoundary : PageBodyContainer.FirstPageTopBoundary;
        public double BottomBoundary => PageBodyContainer.NonLastPageBottomBoundary;

        public double YCursor { get; set; }
        public int CurrentPage { get; set; }

        private readonly XSize _pageSize;
        
        public PdfDocumentManager(Guid messageId, PdfDocument pdf, XSize pageSize, ContainerModel pageBodyContainer)
        {
            MessageId = messageId;
            Pdf = pdf; _pageSize = pageSize;
            PageBodyContainer = pageBodyContainer;
            AddPage();
        }
        

        public void AddPage(Action<PdfDocumentManager> action = null)
        {
            var page = Pdf.AddPage();
            page.Height = _pageSize.Height;
            page.Width = _pageSize.Width;
            YCursor = TopBoundary;
            action?.Invoke(this);
        }
    }
}