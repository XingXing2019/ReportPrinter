using System;
using System.Collections.Generic;
using NUnit.Framework;
using RaphaelLibrary.Code.Common.SqlVariableCacheManager;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.Manager;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using RaphaelLibrary.Code.Render.Label.Renderer;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.Renderer
{
    public class LabelSqlVariableRendererTest : TestBase
    {
        [Test]
        [TestCase("^A0N,20,20^FO41,367^FD%%%<SqlVariable Name=\"TestId\"/>%%%^FS", true)]
        [TestCase("^A0N,20,20^FO41,367^FD%%%<SqlVariable />%%% Name=\"TestId\"/>%%%^FS", false)]
        [TestCase("^A0N,20,20^FO41,367^FD%%%<SqlVariable />%%%^FS", false)]
        [TestCase("^A0N,20,20^FO41,367^FD%%%<SqlVariable Name=\"\"/>%%%^FS", false)]
        [TestCase("^A0N,20,20^FO41,367^FD%%%<SqlVariable Name=\"TestId\"/>%%%/>%%%^FS", false)]
        [TestCase("^A0N,20,20^FO41,367^FD%%%<SqlVariable Name=\"TestId\" %%%<SqlVariable />%%%^FS", false)]
        public void TestReadLine(string line, bool expectedRes)
        {
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);

            try
            {
                var renderer = new LabelSqlVariableRenderer(0);
                var actualRes = renderer.ReadLine(line, deserializer, LabelElementHelper.S_SQL_VARIABLE);

                Assert.AreEqual(expectedRes, actualRes);
                if (expectedRes)
                {
                    var placeHolders = GetPrivateField<List<PlaceHolderBase>>(renderer, "PlaceHolders");

                    Assert.AreEqual(1, placeHolders.Count);
                    var placeHolder = placeHolders[0] as SqlVariablePlaceHolder;
                    Assert.IsNotNull(placeHolder);

                    var name = GetPrivateField<string>(placeHolder, "_name");
                    Assert.AreEqual("TestId", name);
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
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);

            var line = "^A0N,20,20^FO41,367^FD%%%<SqlVariable Name=\"TestId\"/>%%%^FS";
            var renderer = new LabelSqlVariableRenderer(0);

            try
            {
                var isSuccess = renderer.ReadLine(line, deserializer, LabelElementHelper.S_SQL_VARIABLE);
                Assert.IsTrue(isSuccess);

                var cloned = renderer.Clone();
                AssertObject(renderer, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(true)]
        [TestCase(false, "WrongSqlVariableName")]
        [TestCase(false, "WrongMessageId")]
        public void TestTryRenderLabel(bool expectedRes, string operation = "")
        {
            var messageId = Guid.NewGuid();
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            string name = "TestId", value = "Value";
            
            var cacheManagerType = AppConfig.Instance.SqlVariableCacheManagerType;
            var cacheManager = SqlVariableCacheManagerFactory.CreateSqlVariableCacheManager(cacheManagerType);
            
            var sqlVariables = new Dictionary<string, SqlVariable>
            {
                { name, new SqlVariable { Name = name, Value = value } }
            };
            
            cacheManager.StoreSqlVariables(messageId, sqlVariables);

            var renderer = new LabelSqlVariableRenderer(0);
            var line = "^A0N,20,20^FO41,367^FD%%%<SqlVariable Name=\"TestId\"/>%%%^FS";

            if (operation == "WrongSqlVariableName")
                line = line.Replace("TestId", "WrongId");
            else if (operation == "WrongMessageId")
                messageId =Guid.NewGuid();

            try
            {
                var isSuccess = renderer.ReadLine(line, deserializer, LabelElementHelper.S_SQL_VARIABLE);
                Assert.IsTrue(isSuccess);

                var manager = new LabelManager(new[] { line }, messageId);
                var actualRes = renderer.TryRenderLabel(manager);

                Assert.AreEqual(expectedRes, actualRes);
                if (expectedRes)
                {
                    var expectedLine = $"^A0N,20,20^FO41,367^FD{value}^FS";
                    Assert.AreEqual(expectedLine, manager.Lines[0]);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}