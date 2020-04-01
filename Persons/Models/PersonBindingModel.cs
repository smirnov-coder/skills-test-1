using System;
using System.ComponentModel.DataAnnotations;

namespace Persons.Models
{
    public class PersonBindingModel
    {
        public string Name { get; set; }

        public DateTime? BirthDay { get; set; }
    }
}

