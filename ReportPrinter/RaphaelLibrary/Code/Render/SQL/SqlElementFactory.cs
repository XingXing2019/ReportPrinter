using System;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Init.SQL;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.SQL
{
    public class SqlElementFactory
    {
        public static SqlElementBase CreateSqlElement(string name)
        {
            var procName = $"SqlElementFactory.{nameof(CreateSqlElement)}";

            if (name == XmlElementName.SQL)
                return new Sql();
            else if (name == XmlElementName.SQL_TEMPLATE)
                return new SqlTemplate();
            else
            {
                var error = $"Invalid name: {name} for sql element";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}