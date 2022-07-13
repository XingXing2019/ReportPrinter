using System;
using System.Collections.Generic;
using RaphaelLibrary.Code.Render.PDF.Model;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Common
{
    public interface ISqlExecutor
    {
        bool TryExecute(Guid messageId, SqlResColumn sqlResColumn, out string res, bool useCache = true, KeyValuePair<string, SqlVariable> extraSqlVariable = default);
        bool TryExecute(Guid messageId, List<SqlResColumn> sqlResColumnList, out List<Dictionary<string, string>> res, bool useCache = true, KeyValuePair<string, SqlVariable> extraSqlVariable = default);
    }
}