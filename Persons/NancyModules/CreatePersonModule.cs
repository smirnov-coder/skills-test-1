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
    /// ������ ��� �������� ����� ��������� <see cref="Persons.Entities.Person"/>.
    /// </summary>
    public class CreatePersonModule : NancyModule
    {
        private ICommandHandler<CreatePersonCommand> _commandHandler;

        /// <summary>
        /// ������� ������������� Uri, ������������� �������.
        /// </summary>
        public static string BaseUri { get; set; } = "/api/v1/persons";

        public CreatePersonModule(ICommandHandler<CreatePersonCommand> commandHandler) : base(BaseUri)
        {
            _commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));

            Post("/", _ =>
            {
                // ���������� �������� � ��������� ������.
                NewPersonBindingModel model = this.BindAndValidate<NewPersonBindingModel>();

                // ���� ������ ������ �� �������, ������� ����� 400 BadRequest (+������ ��������� � ���� JSON).
                if (!ModelValidationResult.IsValid)
                {
                    return Response.AsJson(ModelValidationResult)
                        .WithStatusCode(HttpStatusCode.BadRequest);
                }

                // ������� ������� CQRS � ��������� �.
                var command = new CreatePersonCommand(model.Name, model.BirthDay.Value);
                CommandResult commandResult = _commandHandler.Handle(command);

                // ��������� ��������� ���������� �������. ���� ������� �� ���������� ��������� (��������� �������� 
                // �� �������), ������� ����� 422 Unprocessable Entity (+������ � ���� JSON).
                if (!commandResult.Succeeded)
                {
                    return Response.AsJson(new { errors = commandResult.Value })
                        .WithStatusCode(HttpStatusCode.UnprocessableEntity);
                }

                // ���� �� ���������, ������� ����� 201 Created c ���������� Location.
                return new Response().WithStatusCode(HttpStatusCode.Created)
                    .WithHeader("Location", $"{BaseUri}/{commandResult.Value}");
            });
        }
    }
}

