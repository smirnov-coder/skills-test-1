using Persons.Abstractions;
using Persons.Entities;
using Persons.Logging;
using Persons.Repositories;
using System;

namespace Persons.Commands
{
    public class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand>
    {
        private IPersonRepository _personRepository;
        private static readonly ILog _logger = LogProvider.For<CreatePersonCommandHandler>();

        // Многими разработчиками осуждается и неприветствуется возвращение каких-либо данных командой CQRS,
        // но я не вижу ничего плохого и противозаконного в том, чтобы таким ненавязчивым способом вернуть ID
        // созданной сущности.
        public Guid CreatedPersonId { get; protected set; } = Guid.Empty;

        public CreatePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        public void Handle(CreatePersonCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            Person person = new Person(command.Name, command.BirthDay);
            _personRepository.Insert(person);
            CreatedPersonId = person.Id;
            _logger.InfoFormat($"Person создан успешно. Person ID = {person.Id}.");
        }
    }
}
