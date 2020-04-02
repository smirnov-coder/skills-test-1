using System;

namespace Persons.Abstractions
{
    /// <summary>
    /// Обработчик команды CQRS.
    /// </summary>
    /// <typeparam name="T">Тип данных объекта команды.</typeparam>
    public interface ICommandHandler<T>
    {
        /// <summary>
        /// Обрабатывает команду.
        /// </summary>
        /// <param name="command">Объект команды типа <typeparamref name="T"/>.</param>
        /// <returns>Результат выполнени¤ команды в виде объекта <see cref="CommandResult"/>.</returns>
        CommandResult Handle(T command);
    }
}
