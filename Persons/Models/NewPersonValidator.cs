using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons.Models
{
    public class NewPersonValidator : AbstractValidator<NewPersonBindingModel>
    {
        public NewPersonValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage($"Поле '{nameof(NewPersonBindingModel.Name)}' не может быть пустой строкой.");

            RuleFor(x => x.BirthDay).NotNull()
                .WithMessage($"Поле '{nameof(NewPersonBindingModel.BirthDay)}' не может быть пустым.");
        }
    }
}
