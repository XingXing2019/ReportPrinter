using GreenPipes.Caching;
using System.Collections.Generic;
using System.Data;
using System;

namespace RaphaelLibrary.Code.Common.SqlResultCacheManager
{
    public interface ISqlResultCacheManager
    {
        void StoreSqlResult(Guid messageId, string sqlId, DataTable sqlResult);

        bool TryGetSqlResult(Guid messageId, string sqlId, out DataTable sqlResult);

        void RemoveSqlResult(Guid messageId);

        void Reset();
    }
}