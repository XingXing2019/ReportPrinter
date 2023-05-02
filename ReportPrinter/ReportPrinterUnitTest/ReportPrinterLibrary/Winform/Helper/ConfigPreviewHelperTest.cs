using System;
using System.Collections.Generic;
using NUnit.Framework;
using ReportPrinterLibrary.Code.Helper;
using ReportPrinterLibrary.Code.Winform.Configuration;

namespace ReportPrinterUnitTest.ReportPrinterLibrary.Winform.Helper
{
    public class ConfigPreviewHelperTest
    {
        [Test]
        public void TestGeneratePreview()
        {
            var obj = new SqlConfigData
            {
                Id = "Test SQL ID",
                DatabaseId = "Test Database ID",
                Query = "Test Query",
                SqlVariableConfigs = new List<SqlVariableConfigData>
                {
                    new SqlVariableConfigData { Name = "Test Variable 1" },
                    new SqlVariableConfigData { Name = "Test Variable 2" },
                }
            };

            var expectedXml = "<Sql Id=\"Test SQL ID\" DatabaseId=\"Test Database ID\"><Query>Test Query</Query><Variable Name=\"Test Variable 1\" /><Variable Name=\"Test Variable 2\" /></Sql>";

            try
            {
                var xml = ConfigPreviewHelper.GeneratePreview(obj).Replace("\r\n  ", "").Replace("\r\n", "");
                Assert.AreEqual(expectedXml, xml);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}