using System;
using System.Collections.Generic;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Database
{
    public class DatabaseManager
    {
        private static readonly object _lock = new object();
        private readonly DatabaseConnectionList _databaseConnectionList;

        private static DatabaseManager _instance;
        public static DatabaseManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DatabaseManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private DatabaseManager()
        {
            var procName = $"DatabaseManager.{nameof(Instance)}";

            var databaseConfigList = AppConfig.Instance.DatabaseConfigList;
            var databaseConnections = new Dictionary<string, DatabaseConnection>();
            foreach (var config in databaseConfigList)
            {
                if (databaseConnections.ContainsKey(config.Id))
                {
                    var error = $"Duplicate database id: {config.Id} detected";
                    Logger.Error(error, procName);
                    throw new ApplicationException(error);
                }
                databaseConnections.Add(config.Id, new DatabaseConnection(config.Id, config.DatabaseName, config.ConnectionString));
            }

            _databaseConnectionList = new DatabaseConnectionList(databaseConnections);
        }

        public bool TryGetConnectionString(string id, out string connectionString)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryGetConnectionString)}";

            connectionString = string.Empty;
            if (!_databaseConnectionList.TryGetDatabaseConnection(id, out var databaseConnection))
                return false;

            connectionString = databaseConnection.ConnectionString;

            Logger.Info($"Return connection string for {id}: {connectionString}", procName);
            return true;
        }

        public void Reset()
        {
            _instance = new DatabaseManager();
        }
    }
}