using System;

namespace RaphaelLibrary.Code.Common
{
    public interface ISqlExecutor
    {
        bool TryExecute(Guid messageId, string sqlResColumn, out string res);
    }
}