namespace ReportPrinterDatabase.Code.Database
{
    public class DatabaseConnection
    {
        public string Id { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

        public DatabaseConnection(string id, string databaseName, string connectionString)
        {
            Id = id;
            DatabaseName = databaseName;
            ConnectionString = connectionString;
        }
    }
}