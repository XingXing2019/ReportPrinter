using System;
using System.Collections.Generic;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.PDF.Structure;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Render.PDF.Manager
{
    public class PdfDocumentManager
    {
        public PdfDocument Pdf { get; }
        public Guid MessageId { get; }

        public ContainerModel ReportHeaderContainer { get; }
        public ContainerModel PageHeaderContainer { get; }
        public ContainerModel PageBodyContainer { get; }
        public ContainerModel PageFooterContainer { get; }
        public ContainerModel ReportFooterContainer { get; }


        public double YCursor { get; set; }
        public int CurrentPage { get; set; }
        public KeyValuePair<string, SqlVariable> ExtraSqlVariable { get; set; }

        private readonly XSize _pageSize;

        public PdfDocumentManager(Guid messageId, PdfDocument pdf, XSize pageSize, Dictionary<PdfStructure, ContainerModel> pdfStructureSizeList)
        {
            MessageId = messageId;
            Pdf = pdf; 
            _pageSize = pageSize;

            ReportHeaderContainer = pdfStructureSizeList[PdfStructure.PdfReportHeader];
            PageHeaderContainer = pdfStructureSizeList[PdfStructure.PdfPageHeader];
            PageBodyContainer = pdfStructureSizeList[PdfStructure.PdfPageBody];
            PageFooterContainer = pdfStructureSizeList[PdfStructure.PdfPageFooter];
            ReportFooterContainer = pdfStructureSizeList[PdfStructure.PdfReportFooter];

            AddPage();
        }
        

        public void AddPage(Action<PdfDocumentManager> action = null)
        {
            var page = Pdf.AddPage();
            page.Height = _pageSize.Height;
            page.Width = _pageSize.Width;
            YCursor = Pdf.PageCount > 1 ? PageBodyContainer.NonFirstPageTopBoundary : PageBodyContainer.FirstPageTopBoundary;
            action?.Invoke(this);
        }
    }
}