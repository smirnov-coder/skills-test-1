using Persons.Entities;
using System;

namespace Persons.Repositories
{
    public interface IPersonRepository
    {
        Person Find(Guid id);

        void Insert(Person item);
    }
}
