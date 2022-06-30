using System.Collections.Generic;

namespace ReportPrinterDatabase.Code.Database
{
    public class DatabaseConnectionList
    {
        private readonly Dictionary<string, DatabaseConnection> _databaseConnections;

        public DatabaseConnectionList(Dictionary<string, DatabaseConnection> databaseConnections)
        {
            _databaseConnections = databaseConnections;
        }
        
        public DatabaseConnection GetDatabaseConnection(string id)
        {
            return _databaseConnections.GetValueOrDefault(id, null);
        }
    }
}