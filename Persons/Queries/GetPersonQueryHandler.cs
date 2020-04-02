using Persons.Abstractions;
using Persons.Entities;
using Persons.Models;
using Persons.Repositories;
using System;

namespace Persons.Queries
{
    /// <summary>
    /// Обработчки запроса <see cref="GetPersonQuery"/>.
    /// </summary>
    public class GetPersonQueryHandler : IQueryHandler<GetPersonQuery, PersonDto>
    {
        private IPersonRepository _personRepository;

        public GetPersonQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        public PersonDto Handle(GetPersonQuery query)
        {
            Person person = _personRepository.Find(query.PersonId);
            if (person == null)
                return null;
            // Согласно заданию, обработчик возвращает не сущность, а плоские данные PersonDto.
            return new PersonDto
            {
                PersonId = person.Id,
                Name = person.Name,
                BirthDay = person.BirthDay,
                Age = person.Age
            };
        }
    }
}
