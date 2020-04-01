using Nancy;
using Persons.Abstractions;
using Persons.Models;
using Persons.Queries;
using System;

namespace Persons.NancyModules
{
    public class GetPersonModule : NancyModule
    {
        private IQueryHandler<GetPersonQuery, PersonDto> _queryHandler;

        public static string BaseUri { get; set; } = "/api/v1/persons";

        public GetPersonModule(IQueryHandler<GetPersonQuery, PersonDto> queryHandler) : base(BaseUri)
        {
            _queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));

            Get("/{id}", parameters =>
            {
                PersonDto person = _queryHandler.Handle(new GetPersonQuery(parameters.Id));
                if (person == null)
                    return HttpStatusCode.NotFound;
                return Response.AsJson(person);
            });
        }
    }
}
