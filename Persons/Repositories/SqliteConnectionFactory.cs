using System;
using System.Data.SQLite;

namespace Persons.Repositories
{
    public class SqliteConnectionFactory
    {
        public SQLiteConnection CreateConnection()
        {
            // Строка подключения для SQLite InMemory Database, разделяемой множеством подключений.
            string connectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder(connectionString)
            {
                // Guid-значения будут хранится в SQLite  в виде строки TEXT.
                BinaryGUID = false
            };
            return new SQLiteConnection(builder.ConnectionString);
        }
    }
}
