using Dapper;
using Persons.Entities;
using System;
using System.Data.SQLite;

namespace Persons.Repositories
{
    /// <inheritdoc cref="IPersonRepository"/>
    public class PersonRepository : IPersonRepository, IDisposable
    {
        private SQLiteConnection _connection;

        public PersonRepository(SqliteConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.CreateConnection();
            _connection.Open();
        }

        public void Dispose()
        {
            ((IDisposable)_connection)?.Dispose();
        }

        public Person Find(Guid id)
        {
            string sql = "SELECT * FROM [Persons] WHERE [Id]=@id";
            return _connection.QuerySingleOrDefault<Person>(sql, new { id });
        }

        public void Insert(Person item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            string sql = @"
                INSERT INTO [Persons] ([Id], [Name], [BirthDay])
                VALUES (@id, @name, @birthDay)";
            _connection.Execute(sql, new { id = item.Id, name = item.Name, birthDay = item.BirthDay });
        }
    }
}
