using Dapper;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Serialization.JsonNet;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using Persons.Abstractions;
using Persons.Commands;
using Persons.Models;
using Persons.Queries;
using Persons.Repositories;
using System;
using System.Collections.Generic;

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

            // Заменим встроенный JSON-serializer на serializer на основе Newtonsoft.Json, т.к. встроенный плохо
            // работает с типом DateTime - ему нужно обязательно указывать время, хотя бы нулевое. Например, вместо
            // "1977-07-07" нужно передавать "1977-07-07T00:00:00".
            container.Register<ISerializer, JsonNetSerializer>();
            container.Register<IBodyDeserializer, JsonNetBodyDeserializer>();

            // Заменим встроенный Guid-маппер на пользовательский.
            SqlMapper.AddTypeHandler(new SqliteGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));
        }
    }
}
