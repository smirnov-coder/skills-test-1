using Dapper;
using Nancy;
using Nancy.TinyIoc;
using Persons.Abstractions;
using Persons.Commands;
using Persons.Models;
using Persons.Queries;
using Persons.Repositories;
using System;

namespace Persons.NancyModules
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            // Добавим несколько пользовательских зависимостей, которые NancyFx не смог подхватить автоматически.
            container.Register<ICommandHandler<CreatePersonCommand>, CreatePersonCommandHandler>();
            container.Register<IQueryHandler<GetPersonQuery, PersonDto>, GetPersonQueryHandler>();

            // Заменим встроенный Guid-маппер на пользовательский.
            SqlMapper.AddTypeHandler(new SqliteGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));
        }
    }
}
