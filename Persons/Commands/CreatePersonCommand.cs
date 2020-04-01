using System;

namespace Persons.Commands
{
    public class CreatePersonCommand
    {
        public string Name { get; set; }

        public DateTime BirthDay { get; set; }

        public CreatePersonCommand(string name, DateTime birthDay)
        {
            Name = name;
            BirthDay = birthDay;
        }
    }
}
