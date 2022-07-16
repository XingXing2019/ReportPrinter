namespace ReportPrinterDatabase.Code.Database
{
    public class DatabaseConnection
    {
        public string Id { get; }
        public string DatabaseName { get; }
        public string ConnectionString { get; }

        public DatabaseConnection(string id, string databaseName, string connectionString)
        {
            Id = id;
            DatabaseName = databaseName;
            ConnectionString = connectionString;
        }
    }
}