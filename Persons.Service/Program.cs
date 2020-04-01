using Persons.Repositories;
using System;
using Topshelf;

namespace Persons.Service
{
    class Program
    {
        public static void Main()
        {
            // Настройка и запуск сервис-хоста для Self-Hosted NancyFx App.
            var rc = HostFactory.Run(x =>
            {
                x.Service<PersonsService>(service => 
                {
                    service.ConstructUsing(name => new PersonsService(new SqliteConnectionFactory()));
                    service.WhenStarted(personsService => personsService.Start());
                    service.WhenStopped(personsService => personsService.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Topshelf ServiceHost for NancyFx App.");
                x.SetDisplayName("PersonsService");
                x.SetServiceName("PersonsService");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
