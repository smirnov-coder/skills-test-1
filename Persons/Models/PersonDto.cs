using System;

namespace Persons.Models
{
    public class PersonDto
    {
        public Guid PersonId { get; set; }

        public string Name { get; set; }

        public DateTime? BirthDay { get; set; }

        public int? Age { get; set; }
    }
}
