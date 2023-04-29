using System.Collections.Generic;

namespace ReportPrinterDatabase.Code.StoredProcedures
{
    public abstract class StoredProcedureBase
    {
        public string StoredProcedureName { get; }
        public Dictionary<string, object> Parameters { get; }

        protected StoredProcedureBase()
        {
            StoredProcedureName = $"s_{GetType().Name}";
            Parameters = new Dictionary<string, object>();
        }
    }
}