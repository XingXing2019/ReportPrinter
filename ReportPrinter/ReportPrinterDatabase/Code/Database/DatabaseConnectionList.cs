using System.Collections.Generic;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Database
{
    public class DatabaseConnectionList
    {
        private readonly Dictionary<string, DatabaseConnection> _databaseConnections;

        public DatabaseConnectionList(Dictionary<string, DatabaseConnection> databaseConnections)
        {
            _databaseConnections = databaseConnections;
        }
        
        public bool TryGetDatabaseConnection(string id, out DatabaseConnection databaseConnection)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryGetDatabaseConnection)}";

            databaseConnection = null;
            if (!_databaseConnections.ContainsKey(id))
            {
                Logger.Error($"Database connection: {id} does not exist", procName);
                return false;
            }
            databaseConnection = _databaseConnections[id];
            return true;
        }
    }
}