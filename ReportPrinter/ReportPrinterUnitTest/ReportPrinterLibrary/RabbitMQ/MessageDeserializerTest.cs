using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.RabbitMQ.Message;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.ReportPrinterLibrary.RabbitMQ
{
    public class MessageDeserializerTest : TestBase
    {
        
        [Test]
        [TestCaseSource(nameof(DeserializeMessageTestCases))]
        public void TestDeserializeXmlMessage(ReportTypeEnum reportType, bool isSuccess)
        {
            var expectedMessage = CreateMessage(reportType, isSuccess);
            using var writer = new StringWriter();
            var serializer = new XmlSerializer(expectedMessage.GetType());
            serializer.Serialize(writer, expectedMessage);
            var xml = writer.ToString();

            try
            {
                if (reportType == ReportTypeEnum.PDF)
                {
                    var res = MessageDeserializer<PrintPdfReport>.DeserializeXmlMessage(xml, out var actualMessage);
                    Assert.AreEqual(isSuccess, res);

                    if (isSuccess)
                    {
                        AssertHelper.AssetMessage(expectedMessage, actualMessage);
                    }
                }
                else if (reportType == ReportTypeEnum.Label)
                {
                    var res = MessageDeserializer<PrintLabelReport>.DeserializeXmlMessage(xml, out var actualMessage);
                    Assert.AreEqual(isSuccess, res);

                    if (isSuccess)
                    {
                        AssertHelper.AssetMessage(expectedMessage, actualMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCaseSource(nameof(DeserializeMessageTestCases))]
        public void TestDeserializeJsonMessage(ReportTypeEnum reportType, bool isSuccess)
        {
            var expectedMessage = CreateMessage(reportType);
            var json = JsonConvert.SerializeObject(expectedMessage);

            if (!isSuccess)
            {
                json += "}";
            }

            try
            {
                if (reportType == ReportTypeEnum.PDF)
                {
                    var res = MessageDeserializer<PrintPdfReport>.DeserializeJsonMessage(json, out var actualMessage);
                    Assert.AreEqual(isSuccess, res);

                    if (isSuccess)
                    {
                        AssertHelper.AssetMessage(expectedMessage, actualMessage);
                    }
                }
                else if (reportType == ReportTypeEnum.Label)
                {
                    var res = MessageDeserializer<PrintLabelReport>.DeserializeJsonMessage(json, out var actualMessage);
                    Assert.AreEqual(isSuccess, res);

                    if (isSuccess)
                    {
                        AssertHelper.AssetMessage(expectedMessage, actualMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        #region Helper

        private static object[] DeserializeMessageTestCases =
        {
            new object[] { ReportTypeEnum.PDF, true},
            new object[] { ReportTypeEnum.PDF, false},
            new object[] { ReportTypeEnum.Label, true},
            new object[] { ReportTypeEnum.Label, false}
        };

        #endregion
    }
}