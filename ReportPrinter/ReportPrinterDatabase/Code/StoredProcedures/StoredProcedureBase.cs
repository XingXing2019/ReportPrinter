using System.Collections.Generic;

namespace ReportPrinterDatabase.Code.StoredProcedures
{
    public abstract class StoredProcedureBase
    {
        public string StoredProcedureName { get; set; }
        public Dictionary<string, object> Parameters { get; }

        protected StoredProcedureBase()
        {
            StoredProcedureName = this.GetType().Name;
            Parameters = new Dictionary<string, object>();
        }
    }
}