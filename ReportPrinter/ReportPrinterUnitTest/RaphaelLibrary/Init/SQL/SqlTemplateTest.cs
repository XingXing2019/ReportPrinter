﻿using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.SQL;

namespace ReportPrinterUnitTest.RaphaelLibrary.Init.SQL
{
    public class SqlTemplateTest : TestBase
    {
        private const string S_FILE_PATH = @".\RaphaelLibrary\Init\SQL\TestFile\SqlTemplate\ValidTemplate.xml";

        [Test]
        [TestCase(true)]
        [TestCase(false, "RemoveTemplateId")]
        [TestCase(false, "RemoveSqls")]
        [TestCase(false, "RemoveSqlId")]
        [TestCase(false, "DuplicateSqlId")]
        public void TestReadXml(bool expectedRes, string operation = "")
        {
            var filePath = S_FILE_PATH;
            var replaceFile = !string.IsNullOrEmpty(operation);

            filePath = ModifyTestFile(filePath, operation);
            var node = TestFileHelper.GetXmlNode(filePath);
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
            var filePath = S_FILE_PATH;
            var node = TestFileHelper.GetXmlNode(filePath);
            var sqlTemplate = new SqlTemplate();

            var isSuccess = sqlTemplate.ReadXml(node);
            Assert.IsTrue(isSuccess);

            try
            {
                var cloned = sqlTemplate.Clone();
                AssertHelper.AssertObject(sqlTemplate, cloned);
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
            var filePath = S_FILE_PATH;
            var node = TestFileHelper.GetXmlNode(filePath);
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


        #region Helper

        private string ModifyTestFile(string filePath, string operation)
        {
            if (operation == "RemoveTemplateId")
                filePath = TestFileHelper.RemoveAttributeOfXmlFile(filePath, "SqlTemplate", "Id");
            else if (operation == "RemoveSqls")
                filePath = TestFileHelper.RemoveXmlNodeOfXmlFile(filePath, "Sql");
            else if (operation == "RemoveSqlId")
                filePath = TestFileHelper.RemoveAttributeOfXmlFile(filePath, "Sql", "Id");
            else if (operation == "DuplicateSqlId")
                filePath = TestFileHelper.ReplaceAttributeOfXmlFile(filePath, "Sql", "Id", "DuplicateId");

            return filePath;
        }

        #endregion
    }
}