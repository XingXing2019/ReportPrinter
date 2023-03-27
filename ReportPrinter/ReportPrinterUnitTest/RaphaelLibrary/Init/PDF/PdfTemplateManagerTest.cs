using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RaphaelLibrary.Code.Init;
using RaphaelLibrary.Code.Init.PDF;

namespace ReportPrinterUnitTest.RaphaelLibrary.Init.PDF
{
    public class PdfTemplateManagerTest : TestBase
    {
        private const string S_FILE_PATH = @".\RaphaelLibrary\Init\PDF\TestFile\PdfTemplateManager\ValidConfig.xml";

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
        [TestCase(false, "RemovePdfTemplate")]
        [TestCase(false, "DuplicateTemplate")]
        [TestCase(false, "InvalidTemplatePath")]
        public void TestReadXml(bool expectedRes, string operation = "")
        {
            var filePath = S_FILE_PATH;
            var replaceFile = !string.IsNullOrEmpty(operation);

            filePath = ModifyTestFile(filePath, operation);
            var node = TestFileHelper.GetXmlNode(filePath);

            try
            {
                var actualRes = PdfTemplateManager.Instance.ReadXml(node);
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

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestTryGetReportTemplate(bool expectedRes)
        {
            var filePath = S_FILE_PATH;
            var node = TestFileHelper.GetXmlNode(filePath);
            var isSuccess = PdfTemplateManager.Instance.ReadXml(node);
            Assert.IsTrue(isSuccess);

            if (!expectedRes)
            {
                var reportTemplateList = GetPrivateField<Dictionary<string, TemplateBase>>(PdfTemplateManager.Instance, "ReportTemplateList");
                reportTemplateList.Clear();
            }

            try
            {
                var isExist = PdfTemplateManager.Instance.TryGetReportTemplate("EmployeeReport", out var reportTemplate);
                Assert.AreEqual(expectedRes, isExist);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void TestReset()
        {
            var filePath = S_FILE_PATH;
            var node = TestFileHelper.GetXmlNode(filePath);
            var isSuccess = PdfTemplateManager.Instance.ReadXml(node);
            Assert.IsTrue(isSuccess);

            try
            {
                var reportTemplateList = GetPrivateField<Dictionary<string, TemplateBase>>(PdfTemplateManager.Instance, "ReportTemplateList");
                Assert.IsTrue(reportTemplateList.Count != 0);

                PdfTemplateManager.Instance.Reset();

                reportTemplateList = GetPrivateField<Dictionary<string, TemplateBase>>(PdfTemplateManager.Instance, "ReportTemplateList");
                Assert.IsTrue(reportTemplateList.Count == 0);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        #region Helper

        private string ModifyTestFile(string filePath, string operation)
        {
            if (operation == "RemovePdfTemplate")
                filePath = TestFileHelper.RemoveXmlNodeOfXmlFile(filePath, "PdfTemplate", "PdfTemplateList");
            else if (operation == "DuplicateTemplate")
            {
                var innerText = TestFileHelper.GetInnerTextOfXmlFile(filePath, "PdfTemplate", "PdfTemplateList");
                filePath = TestFileHelper.AppendXmlNodeToXmlFile(filePath, "PdfTemplateList", "PdfTemplate", innerText);
            }
            else if (operation == "InvalidTemplatePath")
                filePath = TestFileHelper.ReplaceInnerTextOfXmlFile(filePath, "PdfTemplate", "PdfTemplateList", "InvalidPath");

            return filePath;
        }

        #endregion
    }
}