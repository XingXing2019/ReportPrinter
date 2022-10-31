using System;
using System.Collections.Generic;
using NUnit.Framework;
using RaphaelLibrary.Code.Common.SqlVariableCacheManager;
using RaphaelLibrary.Code.Render.Label.Manager;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.PlaceHolder
{
    public class SqlVariablePlaceHolderTest : PlaceHolderTestBase
    {
        private const string S_PLACE_HOLDER = "%%%<SqlVariable Name=\"VariableName\" />%%%";

        [Test]
        [TestCase(true, "Name")]
        [TestCase(false, "Name")]
        public void TestTryGetPlaceHolderValue(bool expectedRes, string name)
        {
            var messageId = Guid.NewGuid();
            var value = "Value";

            if (expectedRes)
            {
                var cacheManagerType = AppConfig.Instance.SqlVariableCacheManagerType;
                var cacheManager = SqlVariableCacheManagerFactory.CreateSqlVariableCacheManager(cacheManagerType);

                var sqlVariables = new Dictionary<string, SqlVariable>
                {
                    { name, new SqlVariable { Name = name, Value = value } }
                };
                cacheManager.StoreSqlVariables(messageId, sqlVariables);
            }

            var lines = new string[] { S_PLACE_HOLDER };
            var manager = new LabelManager(lines, messageId);

            try
            {
                var sqlVariable = new SqlVariablePlaceHolder(S_PLACE_HOLDER, name);
                var actualRes = sqlVariable.TryReplacePlaceHolder(manager, 0);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    var expectedValue = value;
                    Assert.AreEqual(expectedValue, manager.Lines[0]);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void TestClone()
        {
            var name = "Name";
            var sqlVariable = new SqlVariablePlaceHolder(S_PLACE_HOLDER, name);

            try
            {
                var cloned = sqlVariable.Clone();
                AssertObject(sqlVariable, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}