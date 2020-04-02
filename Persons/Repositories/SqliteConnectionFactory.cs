using System;
using System.Data.SQLite;

namespace Persons.Repositories
{
    /// <summary>
    /// Фабрика подключений к БД SQLite.
    /// </summary>
    public class SqliteConnectionFactory
    {
        public SQLiteConnection CreateConnection()
        {
            // Строка подключения для SQLite InMemory Database, разделяемой множеством подключений.
            string connectionString = "FullUri=file::memory:?cache=shared;";
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder(connectionString)
            {
                // Guid-значения будут хранится в SQLite в виде строки TEXT.
                BinaryGUID = false
            };
            return new SQLiteConnection(builder.ConnectionString);
        }
    }
}
