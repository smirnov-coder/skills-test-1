using System;

namespace Persons.Commands
{
    /// <summary>
    /// Команда CQRS на создание нового объекта класса <see cref="Persons.Entities.Person"/>.
    /// </summary>
    public class CreatePersonCommand
    {
        /// <summary>
        /// Имя человека.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Дата рождения человека.
        /// </summary>
        public DateTime BirthDay { get; protected set; }

        public CreatePersonCommand(string name, DateTime birthDay)
        {
            Name = name;
            BirthDay = birthDay;
        }
    }
}
