using System;
using RaphaelLibrary.Code.Render.Label.Manager;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.PlaceHolder
{
    public class PlaceHolderTestBase : TestBase
    {
        protected LabelManager CreateLabelManager(string placeholder, Guid messageId)
        {
            var lines = new string[] { placeholder };
            var manager = new LabelManager(lines, messageId);

            return manager;
        }
    }
}