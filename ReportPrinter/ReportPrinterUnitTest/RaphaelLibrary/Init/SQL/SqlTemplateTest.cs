using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.SQL;

namespace ReportPrinterUnitTest.RaphaelLibrary.Init.SQL
{
    public class SqlTemplateTest : TestBase
    {
        [Test]
        [TestCase(true)]
        [TestCase(false, "RemoveTemplateId")]
        [TestCase(false, "RemoveSqls")]
        [TestCase(false, "RemoveSqlId")]
        [TestCase(false, "DuplicateSqlId")]
        public void TestReadXml(bool expectedRes, string operation = "")
        {
            var filePath = @".\RaphaelLibrary\Init\SQL\TestFile\SqlTemplate\ValidTemplate.xml";
            var replaceFile = !string.IsNullOrEmpty(operation);

            if (operation == "RemoveTemplateId")
            {
                filePath = RemoveAttributeOfXmlFile(filePath, "SqlTemplate", "Id");
            }
            else if (operation == "RemoveSqls")
            {
                filePath = RemoveXmlNodeOfXmlFile(filePath, "Sql");
            }
            else if (operation == "RemoveSqlId")
            {
                filePath = RemoveAttributeOfXmlFile(filePath, "Sql", "Id");
            }
            else if (operation == "DuplicateSqlId")
            {
                filePath = ReplaceAttributeOfXmlFile(filePath, "Sql", "Id", "DuplicateId");
            }

            var node = GetXmlNode(filePath);
            var sqlTemplate = new SqlTemplate();

            try
            {
                var actualRes = sqlTemplate.ReadXml(node);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    var isExist = sqlTemplate.TryGetSql("TransactionInfo", out var sql);
                    Assert.IsTrue(isExist);

                    isExist = sqlTemplate.TryGetSql("EmployeeByGender", out sql);
                    Assert.IsTrue(isExist);
                }
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
        public void TestClone()
        {
            var filePath = @".\RaphaelLibrary\Init\SQL\TestFile\SqlTemplate\ValidTemplate.xml";
            var node = GetXmlNode(filePath);
            var sqlTemplate = new SqlTemplate();

            var isSuccess = sqlTemplate.ReadXml(node);
            Assert.IsTrue(isSuccess);

            try
            {
                var cloned = sqlTemplate.Clone();
                AssertObject(sqlTemplate, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestTryGetSql(bool expectedRes)
        {
            var filePath = @".\RaphaelLibrary\Init\SQL\TestFile\SqlTemplate\ValidTemplate.xml";
            var node = GetXmlNode(filePath);
            var sqlTemplate = new SqlTemplate();

            var isSuccess = sqlTemplate.ReadXml(node);
            Assert.IsTrue(isSuccess);

            if (!expectedRes)
            {
                var sqlList = GetPrivateField<Dictionary<string, SqlElementBase>>(sqlTemplate, "_sqlList");
                sqlList.Clear();
            }

            try
            {
                var actualRes = sqlTemplate.TryGetSql("TransactionInfo", out var sql);
                Assert.AreEqual(expectedRes, actualRes);

                actualRes = sqlTemplate.TryGetSql("EmployeeByGender", out sql);
                Assert.AreEqual(expectedRes, actualRes);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}