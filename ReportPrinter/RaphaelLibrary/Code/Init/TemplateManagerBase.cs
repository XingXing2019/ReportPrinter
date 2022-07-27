using System.Collections.Generic;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Init
{
    public abstract class TemplateManagerBase
    {
        protected readonly Dictionary<string, ITemplate> ReportTemplateList;

        protected TemplateManagerBase()
        {
            ReportTemplateList = new Dictionary<string, ITemplate>();
        }

        /// <summary>
        /// Get a copy of pdf template
        /// </summary>
        /// <returns></returns>
        public bool TryGetReportTemplate(string reportTemplateId, out ITemplate reportTemplate)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryGetReportTemplate)}";
            reportTemplate = null;

            if (!ReportTemplateList.ContainsKey(reportTemplateId))
            {
                Logger.Error($"Report template id: {reportTemplateId} does not exist in template manager", procName);
                return false;
            }

            reportTemplate = ReportTemplateList[reportTemplateId].Clone();

            Logger.Debug($"Return a deep clone of pdf template: {reportTemplateId}", procName);
            return true;
        }
    }
}