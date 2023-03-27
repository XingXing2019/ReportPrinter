using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using NUnit.Framework;
using RaphaelLibrary.Code.Common.SqlVariableCacheManager;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Init.PDF;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterDatabase.Code.Database;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;
using ReportPrinterUnitTest.Helper;

namespace ReportPrinterUnitTest
{
    public abstract class TestBase
    {
        protected readonly AssertHelper AssertHelper;
        protected readonly TestFileHelper TestFileHelper;

        protected readonly Dictionary<string, string> ServicePath;
        protected readonly ISqlVariableCacheManager SqlVariableManager;
        
        private readonly Random _random;

        protected TestBase()
        {
            AssertHelper = new AssertHelper();
            TestFileHelper = new TestFileHelper();

            var servicePathList = AppConfig.Instance.ServicePathConfigList;
            ServicePath = servicePathList.ToDictionary(x => x.Id, x => x.Path);

            var sqlVariableManagerType = AppConfig.Instance.SqlVariableCacheManagerType;
            SqlVariableManager = SqlVariableCacheManagerFactory.CreateSqlVariableCacheManager(sqlVariableManagerType);
            
            _random = new Random();
        }

        [TearDown]
        protected void TearDown()
        {
            LabelStructureManager.Instance.Reset();
            LabelTemplateManager.Instance.Reset();
            PdfTemplateManager.Instance.Reset();
            SqlTemplateManager.Instance.Reset();
            DatabaseManager.Instance.Reset();
        }

        protected IPrintReport CreateMessage(ReportTypeEnum reportType, bool isValidMessage = true)
        {
            var messageId = Guid.NewGuid();
            var correlationId = Guid.NewGuid();
            var templateId = "Template1";
            var printerId = "Printer1";
            var numberOfCopy = 3;
            var hasReprintFlag = true;

            var index = _random.Next(100);
            var sqlVariables = new List<SqlVariable>
            {
                new SqlVariable { Name = $"Name{index}", Value = $"Value{index}" },
                new SqlVariable { Name = $"Name{index + 1}", Value = $"Value{index + 1}" },
            };

            reportType = isValidMessage
                ? reportType
                : reportType == ReportTypeEnum.Label ? ReportTypeEnum.PDF : ReportTypeEnum.Label;

            var expectedMessage = PrintReportMessageFactory.CreatePrintReportMessage(reportType.ToString());

            expectedMessage.MessageId = messageId;
            expectedMessage.CorrelationId = correlationId;
            expectedMessage.TemplateId = templateId;
            expectedMessage.PrinterId = printerId;
            expectedMessage.NumberOfCopy = numberOfCopy;
            expectedMessage.HasReprintFlag = hasReprintFlag;
            expectedMessage.SqlVariables = sqlVariables;

            return expectedMessage;
        }
        

        #region Access Private Field
        
        protected T GetPrivateField<T>(object instance, string fieldName)
        {
            var type = instance.GetType();
            var fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var field = (T)fieldInfo?.GetValue(instance);

            return field;
        }

        protected void SetPrivateField<T>(object instance, string fieldName, object value)
        {
            var type = instance.GetType();
            var fieldInfo = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(instance, value);
        }
        
        #endregion
        

        #region Setup

        protected void SetupDummySqlTemplateManager(Dictionary<string, List<string>> sqlDict)
        {
            var sqlTemplateList = GetPrivateField<Dictionary<string, SqlElementBase>>(SqlTemplateManager.Instance, "_sqlTemplateList");

            foreach (var sqlTemplateId in sqlDict.Keys)
            {
                var sqlIds = sqlDict[sqlTemplateId];
                var sqlTemplate = new SqlTemplate();
                var sqlList = GetPrivateField<Dictionary<string, SqlElementBase>>(sqlTemplate, "_sqlList");

                foreach (var sqlId in sqlIds)
                {
                    sqlList.Add(sqlId, new Sql { Id = sqlId });
                }

                sqlTemplateList.Add(sqlTemplateId, sqlTemplate);
            }
        }

        protected void SetupDummyLabelStructureManager(params string[] labelStructureIds)
        {
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            var labelStructureList = GetPrivateField<Dictionary<string, IStructure>>(LabelStructureManager.Instance, "_labelStructureList");

            foreach (var labelStructureId in labelStructureIds)
            {
                var labelStructure = new LabelStructure(labelStructureId, deserializer, LabelElementHelper.LABEL_RENDERER);
                var prop = labelStructure.GetType().GetField("_lines", BindingFlags.NonPublic | BindingFlags.Instance);
                prop?.SetValue(labelStructure, Array.Empty<string>());

                labelStructureList.Add(labelStructureId, labelStructure);
            }
        }

        protected void SetupDummySqlVariableManager(Guid messageId, Dictionary<string, object> sqlVariablesDict)
        {
            var sqlVariables = sqlVariablesDict.ToDictionary(x => x.Key, x => new SqlVariable { Name = x.Key, Value = x.Value.ToString() });
            SqlVariableManager.StoreSqlVariables(messageId, sqlVariables);
        }
        
        protected async Task<SqlTemplate> SetupSqlTest(string filePath, IPrintReport message, bool expectedRes)
        {
            var databaseManager = new PrintReportMessageEFCoreManager();

            if (expectedRes)
            {
                await databaseManager.Post(message);
            }

            var node = TestFileHelper.GetXmlNode(filePath);
            var sqlTemplate = new SqlTemplate();
            var isSuccess = sqlTemplate.ReadXml(node);
            Assert.IsTrue(isSuccess);

            var cacheManagerType = AppConfig.Instance.SqlVariableCacheManagerType;
            var cacheManager = SqlVariableCacheManagerFactory.CreateSqlVariableCacheManager(cacheManagerType);

            var sqlVariables = new Dictionary<string, SqlVariable>
            {
                { "MessageId", new SqlVariable { Name = "MessageId", Value = message.MessageId.ToString() } },
                { "DummyId", new SqlVariable { Name = "DummyId", Value = "DummyId" } },
            };
            cacheManager.StoreSqlVariables(message.MessageId, sqlVariables);

            var sqlTemplateList = GetPrivateField<Dictionary<string, SqlElementBase>>(SqlTemplateManager.Instance, "_sqlTemplateList");
            sqlTemplateList.Add(sqlTemplate.Id, sqlTemplate);

            return sqlTemplate;
        }

        #endregion
    }
}