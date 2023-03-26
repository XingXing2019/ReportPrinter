using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RaphaelLibrary.Code.Init.PDF;

namespace ReportPrinterUnitTest.RaphaelLibrary.Init.PDF
{
    public class PdfTemplateTest : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetupDummySqlTemplateManager(new Dictionary<string, List<string>>
            {
                { "PrintPdfQuery", new List<string> { "Version", "EmployeeByGender", "EmployeeDepartment" } }
            });
        }

        [Test]
        [TestCase(true)]
        [TestCase(false, "RemoveTemplateId")]
        [TestCase(false, "RemoveSavePath")]
        [TestCase(false, "RemoveFileNameSuffix")]
        [TestCase(false, "RemoveTimeout")]
        [TestCase(false, "ReplaceTimeout")]
        [TestCase(false, "RemoveTextType")]
        [TestCase(false, "RemoveBarcodeSqlTemplateId")]
        [TestCase(false, "RemoveImageSource")]
        [TestCase(false, "RemoveAnnotationType")]
        [TestCase(false, "RemoveTableSqlTemplateId")]
        [TestCase(false, "RemoveWaterMarkType")]
        [TestCase(false, "RemoveReprintMarkText")]
        public void TestReadXml(bool expectedRes, string operation = "")
        {
            var filePath = @".\RaphaelLibrary\Init\PDF\TestFile\PdfTemplate\ValidTemplate.xml";
            var replaceFile = !string.IsNullOrEmpty(operation);

            if (operation == "RemoveTemplateId")
                filePath = RemoveAttributeOfXmlFile(filePath, "PdfTemplate", "Id");
            else if (operation == "RemoveSavePath")
                filePath = RemoveAttributeOfXmlFile(filePath, "PdfTemplate", "SavePath");
            else if (operation == "RemoveFileNameSuffix")
                filePath = RemoveAttributeOfXmlFile(filePath, "PdfTemplate", "FileNameSuffix");
            else if (operation == "RemoveTimeout")
                filePath = RemoveAttributeOfXmlFile(filePath, "PdfTemplate", "Timeout");
            else if (operation == "ReplaceTimeout")
                filePath = ReplaceAttributeOfXmlFile(filePath, "PdfTemplate", "Timeout", "InvalidTimeout");
            else if (operation == "RemoveTextType")
                filePath = ReplaceInnerTextOfXmlFile(filePath, "Type", "Text", "");
            else if (operation == "RemoveBarcodeSqlTemplateId")
                filePath = ReplaceInnerTextOfXmlFile(filePath, "SqlTemplateId", "Barcode", "");
            else if (operation == "RemoveImageSource")
                filePath = ReplaceInnerTextOfXmlFile(filePath, "ImageSource", "Image", "");
            else if (operation == "RemoveAnnotationType")
                filePath = ReplaceInnerTextOfXmlFile(filePath, "Type", "Annotation", "");
            else if (operation == "RemoveTableSqlTemplateId")
                filePath = ReplaceInnerTextOfXmlFile(filePath, "SqlTemplateId", "Table", "");
            else if (operation == "RemoveWaterMarkType")
                filePath = ReplaceInnerTextOfXmlFile(filePath, "Type", "WaterMark", "");
            else if (operation == "RemoveReprintMarkText")
                filePath = ReplaceInnerTextOfXmlFile(filePath, "Text", "ReprintMark", "");
            
            var node = GetXmlNode(filePath);
            var pdfTemplate = new PdfTemplate();

            try
            {
                var actualRes = pdfTemplate.ReadXml(node);
                Assert.AreEqual(expectedRes, actualRes);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                if (replaceFile)
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}