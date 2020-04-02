using Persons.Abstractions;
using Persons.Entities;
using Persons.Logging;
using Persons.Repositories;
using System;

namespace Persons.Commands
{
    /// <summary>
    /// Обработчик команды CQRS <see cref="CreatePersonCommand"/>.
    /// </summary>
    /// <inheritdoc/>
    public class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand>
    {
        private IPersonRepository _personRepository;
        private static readonly ILog _logger = LogProvider.For<CreatePersonCommandHandler>();

        public CreatePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        public CommandResult Handle(CreatePersonCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            Person person = new Person(command.Name, command.BirthDay);
            EntityValidationResult validationResult = person.Validate();
            if (validationResult.IsValid)
            {
                _personRepository.Insert(person);
                _logger.InfoFormat($"Person создан успешно. Person ID = {person.Id}.");
                return new CommandResult
                {
                    Succeeded = true,
                    Type = person.Id.GetType(),
                    Value = person.Id
                };
            }
            else
            {
                _logger.ErrorFormat($"Не удалось добавить новый объект Person в репозиторий. Person, созданный на " +
                    $"основе входных данных, не валиден.");
                return new CommandResult
                {
                    Succeeded = false,
                    Type = validationResult.Errors.GetType(),
                    Value = validationResult.Errors
                };
            }
        }
    }
}
