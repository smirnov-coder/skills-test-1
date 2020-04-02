using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Persons.Abstractions;
using Persons.Commands;
using Persons.Models;
using System;

namespace Persons.NancyModules
{
    /// <summary>
    /// Модуль для создания новых сущностей <see cref="Persons.Entities.Person"/>.
    /// </summary>
    public class CreatePersonModule : NancyModule
    {
        private ICommandHandler<CreatePersonCommand> _commandHandler;

        /// <summary>
        /// Базовый относительный Uri, обслуживаемый модулем.
        /// </summary>
        public static string BaseUri { get; set; } = "/api/v1/persons";

        public CreatePersonModule(ICommandHandler<CreatePersonCommand> commandHandler) : base(BaseUri)
        {
            _commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));

            Post("/", _ =>
            {
                // Произвести привязку и валидацию модели.
                NewPersonBindingModel model = this.BindAndValidate<NewPersonBindingModel>();

                // Если данные модели не валидны, вернуть ответ 400 BadRequest (+ошибки валидации в виде JSON).
                if (!ModelValidationResult.IsValid)
                {
                    return Response.AsJson(ModelValidationResult)
                        .WithStatusCode(HttpStatusCode.BadRequest);
                }

                // Создать команду CQRS и выполнить её.
                var command = new CreatePersonCommand(model.Name, model.BirthDay.Value);
                CommandResult commandResult = _commandHandler.Handle(command);

                // Проверить результат выполнения команды. Если команда не отработала нормально (созданная сущность 
                // не валидна), вернуть ответ 422 Unprocessable Entity (+ошибки в виде JSON).
                if (!commandResult.Succeeded)
                {
                    return Response.AsJson(new { errors = commandResult.Value })
                        .WithStatusCode(HttpStatusCode.UnprocessableEntity);
                }

                // Если всё нормально, вернуть ответ 201 Created c заголовком Location.
                return new Response().WithStatusCode(HttpStatusCode.Created)
                    .WithHeader("Location", $"{BaseUri}/{commandResult.Value}");
            });
        }
    }
}

