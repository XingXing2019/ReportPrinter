using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.SQL;

namespace ReportPrinterUnitTest.RaphaelLibrary.Init.SQL
{
    public class SqlTemplateManagerTest : TestBase
    {
        [Test]
        [TestCase(true)]
        [TestCase(false, "RemoveTemplate")]
        [TestCase(false, "ReplaceSqlTemplate")]
        [TestCase(false, "DuplicateSqlTemplate")]
        [TestCase(false, "WrongFilePath")]
        public void TestReadXml(bool expectedRes, string operation = "")
        {
            var filePath = @".\RaphaelLibrary\Init\SQL\TestFile\SqlTemplateManager\ValidConfig.xml";
            var replaceFile = !string.IsNullOrEmpty(operation);

            var tempSqlTemplate = "";
            if (operation == "RemoveTemplate")
            {
                filePath = RemoveXmlNodeOfXmlFile(filePath, "SqlTemplate");
            }
            else if (operation == "ReplaceSqlTemplate")
            {
                var sqlTemplatePath = @".\RaphaelLibrary\Init\SQL\TestFile\SqlTemplate\ValidTemplate.xml";
                tempSqlTemplate = RemoveAttributeOfXmlFile(sqlTemplatePath, "Sql", "Id");

                filePath = ReplaceInnerTextOfXmlFile(filePath, "SqlTemplate", tempSqlTemplate);
            }
            else if (operation == "DuplicateSqlTemplate")
            {
                var innerText = @".\RaphaelLibrary\Init\SQL\TestFile\SqlTemplate\ValidTemplate.xml";
                filePath = AppendXmlNodeToXmlFile(filePath, "SqlTemplateList", "SqlTemplate", innerText);
            }
            else if (operation == "WrongFilePath")
            {
                var innerText = "WrongFilePath";
                filePath = ReplaceInnerTextOfXmlFile(filePath, "SqlTemplate", "WrongFilePath");
            }
            
            var node = GetXmlNode(filePath);

            try
            {
                var actualRes = SqlTemplateManager.Instance.ReadXml(node);
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

                if (!string.IsNullOrEmpty(tempSqlTemplate))
                {
                    File.Delete(tempSqlTemplate);
                }
            }
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestTryGetSql(bool expectedRes)
        {
            var filePath = @".\RaphaelLibrary\Init\SQL\TestFile\SqlTemplateManager\ValidConfig.xml";
            var node = GetXmlNode(filePath);
            var isSuccess = SqlTemplateManager.Instance.ReadXml(node);
            Assert.IsTrue(isSuccess);

            if (!expectedRes)
            {
                var sqlTemplateList = GetPrivateField<Dictionary<string, SqlElementBase>>(SqlTemplateManager.Instance, "_sqlTemplateList");
                sqlTemplateList.Clear();
            }

            try
            {
                var isExist = SqlTemplateManager.Instance.TryGetSql("PrintPdfQuery", "TransactionInfo", out var sql);
                Assert.AreEqual(expectedRes, isExist);

                isExist = SqlTemplateManager.Instance.TryGetSql("PrintPdfQuery", "EmployeeByGender", out sql);
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
            var filePath = @".\RaphaelLibrary\Init\SQL\TestFile\SqlTemplateManager\ValidConfig.xml";
            var node = GetXmlNode(filePath);
            var isSuccess = SqlTemplateManager.Instance.ReadXml(node);
            Assert.IsTrue(isSuccess);

            try
            {
                var sqlTemplateList = GetPrivateField<Dictionary<string, SqlElementBase>>(SqlTemplateManager.Instance, "_sqlTemplateList");
                Assert.IsTrue(sqlTemplateList.Count != 0);

                SqlTemplateManager.Instance.Reset();

                sqlTemplateList = GetPrivateField<Dictionary<string, SqlElementBase>>(SqlTemplateManager.Instance, "_sqlTemplateList");
                Assert.IsTrue(sqlTemplateList.Count == 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}