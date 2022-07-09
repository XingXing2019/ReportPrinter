using System;
using System.Collections.Generic;
using RaphaelLibrary.Code.Render.PDF.Model;

namespace RaphaelLibrary.Code.Common
{
    public interface ISqlExecutor
    {
        bool TryExecute(Guid messageId, SqlResColumn sqlResColumn, out string res);
        bool TryExecute(Guid messageId, List<SqlResColumn> sqlResColumnList, out List<Dictionary<string, string>> res);
    }
}