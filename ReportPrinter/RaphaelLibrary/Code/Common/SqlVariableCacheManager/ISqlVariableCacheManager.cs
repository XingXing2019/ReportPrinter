using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;
using System.Collections.Generic;
using System;

namespace RaphaelLibrary.Code.Common.SqlVariableCacheManager
{
    public interface ISqlVariableCacheManager
    {
        void StoreSqlVariables(Guid messageId, Dictionary<string, SqlVariable> sqlVariables);

        Dictionary<string, SqlVariable> GetSqlVariables(Guid messageId);

        void RemoveSqlVariables(Guid messageId);

        void Reset();
    }
}