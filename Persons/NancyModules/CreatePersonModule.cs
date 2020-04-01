using Nancy;
using Nancy.ModelBinding;
using Persons.Abstractions;
using Persons.Commands;
using Persons.Logging;
using Persons.Models;
using System;

namespace Persons.NancyModules
{
    public class CreatePersonModule : NancyModule
    {
        private ICommandHandler<CreatePersonCommand> _commandHandler;
        private ILog _logger = LogProvider.For<CreatePersonModule>();

        public static string BaseUri { get; set; } = "/api/v1/persons";

        public CreatePersonModule(ICommandHandler<CreatePersonCommand> commandHandler) : base(BaseUri)
        {
            _commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));

            Post("/", _ =>
            {
                // Привязка модели. К сожалению, встроенный ModelBinder напрочь отказывается принимать DateTime в
                // виде даты "1977-07-07", ему обязательно нужно указывать время, хотя бы нулевое "1977-07-07T00:00:00".
                // Решением данной проблемы будет либо игнорирование принимаемого в запросе значения "BirthDay" и
                // последующий его парсинг вручную, либо, что более правильно - написание кастомного ModelBinder. :(
                PersonBindingModel model = null;
                try
                {
                    model = this.Bind<PersonBindingModel>();
                }
                catch (Exception ex)
                {
                    _logger.ErrorException("Произошла ошибка привязки модели. Скорее всего из-за неправильного " +
                        "формата значения 'BirthDay'.", ex);
                }

                // Лёгкая ручная валидация модели (заказчиком не огворена и не затребована). :)
                if (string.IsNullOrWhiteSpace(model.Name) || !model.BirthDay.HasValue || model.BirthDay.Value == new DateTime())
                    return HttpStatusCode.BadRequest;

                // Создать команду и обработать её.
                var command = new CreatePersonCommand(model.Name, model.BirthDay.Value);
                _commandHandler.Handle(command);

                // Проверить, что сущность создана успешно и ей присвоен ID.
                Guid createdPersonId = (_commandHandler as CreatePersonCommandHandler).CreatedPersonId;
                if (createdPersonId == Guid.Empty)
                    return HttpStatusCode.UnprocessableEntity;

                return new Response().WithStatusCode(HttpStatusCode.Created)
                    .WithHeader("Location", $"{BaseUri}/{createdPersonId}");
            });
        }
    }
}

