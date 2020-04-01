using Nancy.Hosting.Self;
using Persons.Repositories;
using Serilog;
using System;
using System.Data;

namespace Persons.Service
{
    public class PersonsService : IDisposable
    {
        private NancyHost _nancyHost;

        // SQLite InMemory Database будет жить в памяти, пока живо хотя бы одно соединение к ней.
        private IDbConnection _masterConnection;

        // Endpoint, по которому будет доступно наше Self-Hosted NancyFx App.
        public Uri Uri { get; set; } = new Uri("http://localhost:9000");

        public PersonsService(SqliteConnectionFactory connectionFactory)
        {
            _masterConnection = connectionFactory.CreateConnection();
        }

        public void Dispose()
        {
            ((IDisposable)_nancyHost)?.Dispose();
            ((IDisposable)_masterConnection)?.Dispose();
        }

        public void Start()
        {
            ConfigureDatabase();
            ConfigureLogging();
            _nancyHost = new NancyHost(Uri);
            _nancyHost.Start();
        }

        private void ConfigureDatabase()
        {
            _masterConnection.Open();
            var command = _masterConnection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS [Persons] (
                    [Id] TEXT PRIMARY KEY,
                    [Name] TEXT,
                    [BirthDay] TEXT
                )";
            command.ExecuteNonQuery();
        }

        private void ConfigureLogging()
        {
            // Настроим Serilog (который будет автоматически подхвачен LibLog и использован в качестве LogProvider).
            var log = new LoggerConfiguration()
                .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({Name:l}) {Message}{NewLine}{Exception}")
                .CreateLogger();
            Log.Logger = log;
        }

        public void Stop()
        {
            _nancyHost?.Stop();
            _masterConnection?.Close();
        }
    }
}
