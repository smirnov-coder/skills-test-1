using Nancy;
using Persons.Abstractions;
using Persons.Models;
using Persons.Queries;
using System;

namespace Persons.NancyModules
{
    /// <summary>
    /// ������ ��� ���������� ��������� <see cref="Persons.Entities.Person"/>.
    /// </summary>
    public class GetPersonModule : NancyModule
    {
        private IQueryHandler<GetPersonQuery, PersonDto> _queryHandler;

        /// <summary>
        /// ������� ������������� Uri, ������������� �������.
        /// </summary>
        public static string BaseUri { get; set; } = "/api/v1/persons";

        public GetPersonModule(IQueryHandler<GetPersonQuery, PersonDto> queryHandler) : base(BaseUri)
        {
            _queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));

            Get("/{id}", parameters =>
            {
                // �.�. � �������� ID �������� Person ������������ Guid, �� ���������� ������� ���������, ��� � �������
                // �� ���������� ������ ������ Guid, ����� ������� ����������.
                Guid.TryParse(parameters.Id, out Guid id);

                PersonDto person = _queryHandler.Handle(new GetPersonQuery(id));

                // ���� �������� �� �������, �� ������� ����� 404 NotFound.
                if (person == null)
                    return HttpStatusCode.NotFound;

                // ����� ������� PersonDto � ���� JSON � ������ 200 OK.
                return Response.AsJson(person);
            });
        }
    }
}
