using System;
using NUnit.Framework;
using RaphaelLibrary.Code.Common.SqlVariableCacheManager;
using RaphaelLibrary.Code.Init.SQL;
using System.Collections.Generic;
using System.Threading.Tasks;
using RaphaelLibrary.Code.Render.Label.Manager;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.PlaceHolder
{
    public class PlaceHolderTestBase : TestBase
    {
        protected const string S_PLACE_HOLDER = "Dummy Place Holder";

        protected LabelManager CreateLabelManager(string placeholder, Guid messageId)
        {
            var lines = new string[] { placeholder };
            var manager = new LabelManager(lines, messageId);

            return manager;
        }
    }
}