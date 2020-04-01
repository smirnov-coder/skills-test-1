using System;

namespace Persons.Abstractions
{
    public interface ICommandHandler<T>
    {
        void Handle(T command);
    }
}
