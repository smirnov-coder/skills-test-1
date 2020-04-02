using System;

namespace Persons.Abstractions
{
    /// <summary>
    /// ���������� ������� CQRS.
    /// </summary>
    /// <typeparam name="T">��� �������.</typeparam>
    public interface ICommandHandler<T>
    {
        /// <summary>
        /// ������������ �������.
        /// </summary>
        /// <param name="command">������ ������� ���� <typeparamref name="T"/>.</param>
        /// <returns>��������� ���������� ������� � ���� ������� <see cref="CommandResult"/>.</returns>
        CommandResult Handle(T command);
    }
}
