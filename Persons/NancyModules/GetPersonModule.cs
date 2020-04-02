using Nancy;
using Persons.Abstractions;
using Persons.Models;
using Persons.Queries;
using System;

namespace Persons.NancyModules
{
    /// <summary>
    /// Модуль для извлечения сущностей <see cref="Persons.Entities.Person"/>.
    /// </summary>
    public class GetPersonModule : NancyModule
    {
        private IQueryHandler<GetPersonQuery, PersonDto> _queryHandler;

        /// <summary>
        /// Базовый относительный Uri, обслуживаемый модулем.
        /// </summary>
        public static string BaseUri { get; set; } = "/api/v1/persons";

        public GetPersonModule(IQueryHandler<GetPersonQuery, PersonDto> queryHandler) : base(BaseUri)
        {
            _queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));

            Get("/{id}", parameters =>
            {
                // Т.к. в качестве ID сущности Person используется Guid, то необходимо сначала проверить, что в запросе
                // на извлечение пришёл именно Guid, иначе получим исключение.
                Guid.TryParse(parameters.Id, out Guid id);

                PersonDto person = _queryHandler.Handle(new GetPersonQuery(id));

                // Если сущность не найдена, то вернуть ответ 404 NotFound.
                if (person == null)
                    return HttpStatusCode.NotFound;

                // Иначе вернуть PersonDto в виде JSON в ответе 200 OK.
                return Response.AsJson(person);
            });
        }
    }
}
