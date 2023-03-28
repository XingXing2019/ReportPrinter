using System;
using NUnit.Framework;
using PdfSharp;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Render.PDF.Helper;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.PDF.Helper
{
    public class PageHelperTest : TestBase
    {
        [Test]
        [TestCase("A0", true, PageSize.A0)]
        [TestCase("A1", true, PageSize.A1)]
        [TestCase("A2", true, PageSize.A2)]
        [TestCase("A3", true, PageSize.A3)]
        [TestCase("A4", true, PageSize.A4)]
        [TestCase("A5", true, PageSize.A5)]
        [TestCase("RA0", true, PageSize.RA0)]
        [TestCase("RA1", true, PageSize.RA1)]
        [TestCase("RA2", true, PageSize.RA2)]
        [TestCase("RA3", true, PageSize.RA3)]
        [TestCase("RA4", true, PageSize.RA4)]
        [TestCase("RA5", true, PageSize.RA5)]
        [TestCase("B0", true, PageSize.B0)]
        [TestCase("B1", true, PageSize.B1)]
        [TestCase("B2", true, PageSize.B2)]
        [TestCase("B3", true, PageSize.B3)]
        [TestCase("B4", true, PageSize.B4)]
        [TestCase("B5", true, PageSize.B5)]
        [TestCase("Quarto", true, PageSize.Quarto)]
        [TestCase("Foolscap", true, PageSize.Foolscap)]
        [TestCase("Executive", true, PageSize.Executive)]
        [TestCase("GovernmentLetter", true, PageSize.GovernmentLetter)]
        [TestCase("Letter", true, PageSize.Letter)]
        [TestCase("Legal", true, PageSize.Legal)]
        [TestCase("Ledger", true, PageSize.Ledger)]
        [TestCase("Tabloid", true, PageSize.Tabloid)]
        [TestCase("Post", true, PageSize.Post)]
        [TestCase("Crown", true, PageSize.Crown)]
        [TestCase("LargePost", true, PageSize.LargePost)]
        [TestCase("Demy", true, PageSize.Demy)]
        [TestCase("Medium ", true, PageSize.Medium )]
        [TestCase("Royal", true, PageSize.Royal)]
        [TestCase("Elephant", true, PageSize.Elephant)]
        [TestCase("DoubleDemy", true, PageSize.DoubleDemy)]
        [TestCase("QuadDemy", true, PageSize.QuadDemy)]
        [TestCase("STMT", true, PageSize.STMT)]
        [TestCase("Folio", true, PageSize.Folio)]
        [TestCase("Statement", true, PageSize.Statement)]
        [TestCase("Size10x14", true, PageSize.Size10x14)]
        [TestCase("InvalidSize", false, -1)]
        public void TestTryGetPageSize(string pageSize, bool expectedRes, PageSize expectedSize)
        {
            try
            {
                var actualRes = PageHelper.TryGetPageSize(pageSize, out var actualSize);
                Assert.AreEqual(expectedRes, actualRes);
                Assert.AreEqual(actualSize, expectedRes ? PageSizeConverter.ToSize(expectedSize) : XSize.Empty);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}