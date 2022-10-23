using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterDatabase.Code.Manager.MessageManager;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.SQL
{
    public class SqlTest : TestBase
    {
        private readonly IMessageManager<IPrintReport> _manager;

        public SqlTest()
        {
            _manager = new PrintReportMessageEFCoreManager();
        }

        [Test]
        [TestCase(true)]
        [TestCase(false, "Sql", "Id", "", "ReplaceAttribute")]
        [TestCase(false, "Sql", "DatabaseId", "", "ReplaceAttribute")]
        [TestCase(false, "Sql", "DatabaseId", "WrongId", "ReplaceAttribute")]
        [TestCase(false, "Sql", "Query", "", "ReplaceInnerText")]
        [TestCase(false, "Variable", "Name", "", "ReplaceAttribute")]
        [TestCase(false, "Sql", "Name", "MessageId", "AppendXmlNode")]
        public void TestReadXml(bool expectedRes, string parentNode = "", string name = "", string value = "", string operation = "")
        {
            var filePath = @".\RaphaelLibrary\Render\SQL\TestFile\ValidSql.xml";

            if (!expectedRes)
            {
                if (operation == "ReplaceAttribute")
                    filePath = ReplaceAttributeOfXmlFile(filePath, parentNode, name, value);
                else if (operation == "ReplaceInnerText")
                    filePath = ReplaceInnerTextOfXmlFile(filePath, name, value);
                else if (operation == "AppendXmlNode")
                {
                    var attributes = new Dictionary<string, string> { { name, value } };
                    filePath = AppendXmlNodeToXmlFile(filePath, parentNode, "Variable", "", attributes);
                }
            }

            var node = GetXmlNode(filePath);

            try
            {
                var sql = new Sql();
                var actualRes = sql.ReadXml(node);

                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    var expectedId = "TestSql";
                    var actualId = sql.Id;
                    Assert.AreEqual(expectedId, actualId);

                    var expectedConnStr = AppConfig.Instance.DatabaseConfigList.First(x => x.Id == "ReportPrinterTest").ConnectionString;
                    var actualConnStr = GetPrivateField<string>(sql, "_connectionString");
                    Assert.AreEqual(expectedConnStr, actualConnStr);

                    var expectedQuery = "SELECT * FROM PrintReportMessage WHERE PRM_MessageId = '%%%MessageId%%%' AND PRM_PrinterId = '%%%PrinterId%%%'";
                    var actualQuery = GetPrivateField<string>(sql, "_query").Trim('\r','\n','\t');
                    Assert.AreEqual(expectedQuery, actualQuery);

                    var sqlVariables = GetPrivateField<Dictionary<string, SqlVariable>>(sql, "_sqlVariables");
                    Assert.AreEqual(2, sqlVariables.Count);
                    var expectedSqlVariable = new SqlVariable { Name = "MessageId" };
                    var actualSqlVariable = sqlVariables["MessageId"];
                    AssertObject(expectedSqlVariable, actualSqlVariable);

                    expectedSqlVariable = new SqlVariable { Name = "PrinterId" };
                    actualSqlVariable = sqlVariables["PrinterId"];
                    AssertObject(expectedSqlVariable, actualSqlVariable);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                if (!expectedRes)
                {
                    File.Delete(filePath);
                }
            }
        }

        [Test]
        public void TestClone()
        {
            var filePath = @".\RaphaelLibrary\Render\SQL\TestFile\ValidSql.xml";

            var node = GetXmlNode(filePath);
            var sql = new Sql();

            try
            {
                var isSuccess = sql.ReadXml(node);
                Assert.IsTrue(isSuccess);

                var cloned = sql.Clone();
                AssertObject(sql, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(true)]
        [TestCase(true, true)]
        [TestCase(false, false, "RemoveSqlVariable")]
        [TestCase(false, false, "ReplaceQuery")]
        [TestCase(false, false, "DeleteRes")]
        [TestCase(false, false, "AddRes")]
        [TestCase(false, false, "ReplaceRes")]
        public async Task TestTryExecute(bool expectedRes, bool hasExtraVariable = false, string operation = "")
        {
            var message = CreateMessage(ReportTypeEnum.PDF);
            var filePath = @".\RaphaelLibrary\Render\SQL\TestFile\ValidSql.xml";
            var replaceFile = false;

            await _manager.Post(message);
            
            SetupDummySqlVariableManager(message.MessageId, new Dictionary<string, object>
            {
                { "MessageId", message.MessageId }, { "PrinterId", message.PrinterId }
            });
            
            if (!expectedRes)
            {
                if (operation == "RemoveSqlVariable")
                {
                    var sqlVariableRepo = GetPrivateField<Dictionary<Guid, Dictionary<string, SqlVariable>>>(SqlVariableManager.Instance, "_sqlVariableRepo");
                    sqlVariableRepo[message.MessageId].Remove("PrinterId");
                }
                else if (operation == "ReplaceQuery")
                {
                    var query = "WrongQuery";
                    filePath = ReplaceInnerTextOfXmlFile(filePath, "Query", query);
                    replaceFile = true;
                }
                else if (operation == "DeleteRes")
                {
                    await _manager.DeleteAll();
                }
                else if (operation == "AddRes")
                {
                    var query = "SELECT * FROM PrintReportMessage";
                    filePath = ReplaceInnerTextOfXmlFile(filePath, "Query", query);
                    replaceFile = true;

                    var tempMessage = CreateMessage(ReportTypeEnum.PDF);
                    await _manager.Post(tempMessage);
                }
                else if (operation == "ReplaceRes")
                {
                    var query = "SELECT PRM_MessageId FROM PrintReportMessage";
                    filePath = ReplaceInnerTextOfXmlFile(filePath, "Query", query);
                    replaceFile = true;
                }
            }

            KeyValuePair<string, SqlVariable> extraSqlVariable = default;
            if (hasExtraVariable)
            {
                var query = "SELECT * FROM PrintReportMessage WHERE PRM_MessageId = '%%%MessageId%%%' AND PRM_TemplateId = '%%%TemplateId%%%'";
                filePath = ReplaceInnerTextOfXmlFile(filePath, "Query", query);
                replaceFile = true;
                extraSqlVariable = new KeyValuePair<string, SqlVariable>("TemplateId", new SqlVariable { Name = "TemplateId", Value = message.TemplateId });
            }

            var sql = new Sql();
            var node = GetXmlNode(filePath);
            var isSuccess = sql.ReadXml(node);
            Assert.IsTrue(isSuccess);

            try
            {
                var sqlResColumn = new SqlResColumn("PRM_ReportType");
                var actualRes = sql.TryExecute(message.MessageId, sqlResColumn, out var strRes, true, extraSqlVariable);

                Assert.AreEqual(expectedRes, actualRes);
                if (expectedRes)
                {
                    Assert.AreEqual(message.ReportType.ToString(), strRes);
                }

                var sqlResColumnList = new List<SqlResColumn>
                {
                    new SqlResColumn("PRM_ReportType"),
                    new SqlResColumn("PRM_NumberOfCopy"),
                };

                // TryExecute with returning dictionary allows more than one row or no row
                if (operation == "AddRes" || operation == "DeleteRes")
                    return;

                actualRes = sql.TryExecute(message.MessageId, sqlResColumnList, out var dictRes);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    Assert.AreEqual(1, dictRes.Count);
                    var row = dictRes[0];

                    Assert.IsTrue(row.ContainsKey("PRM_ReportType"));
                    Assert.AreEqual(message.ReportType.ToString(), row["PRM_ReportType"]);

                    Assert.IsTrue(row.ContainsKey("PRM_NumberOfCopy"));
                    Assert.AreEqual(message.NumberOfCopy.ToString(), row["PRM_NumberOfCopy"]);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
               await _manager.DeleteAll();
               if (replaceFile)
               {
                   File.Delete(filePath);
               }
            }
        }
    }
}