using System;

namespace Persons.Commands
{
    /// <summary>
    /// ������� CQRS �� �������� ������ ������� ������ <see cref="Persons.Entities.Person"/>.
    /// </summary>
    public class CreatePersonCommand
    {
        /// <summary>
        /// ��� ��������.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// ���� �������� ��������.
        /// </summary>
        public DateTime BirthDay { get; protected set; }

        public CreatePersonCommand(string name, DateTime birthDay)
        {
            Name = name;
            BirthDay = birthDay;
        }
    }
}
