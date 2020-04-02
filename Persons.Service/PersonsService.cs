using Nancy.Hosting.Self;
using Persons.Repositories;
using Serilog;
using System;
using System.Data;

namespace Persons.Service
{
    /// <summary>
    /// Windows-сервис (фейковый), в котором хостится Self-hosted NancyFx App.
    /// </summary>
    public class PersonsService : IDisposable
    {
        private NancyHost _nancyHost;
        // SQLite InMemory Database будет жить в памяти, пока живо хотя бы одно подключение к ней.
        private IDbConnection _masterConnection;

        /// <summary>
        /// Endpoint, по которому будет доступно наше Self-Hosted NancyFx App.
        /// </summary>
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

        // Предварительная настройка SQLite InMemory DB.
        private void ConfigureDatabase()
        {
            _masterConnection.Open();
            var command = _masterConnection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS [Persons] (
                    [Id] TEXT PRIMARY KEY,
                    [Name] TEXT NULL,
                    [BirthDay] TEXT NULL
                )";
            command.ExecuteNonQuery();
        }

        // Настройка Serilog (который будет автоматически подхвачен LibLog и использован в качестве LogProvider).
        private void ConfigureLogging()
        {
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
