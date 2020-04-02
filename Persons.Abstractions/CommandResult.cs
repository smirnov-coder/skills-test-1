using System;

namespace Persons.Abstractions
{
    /// <summary>
    /// Результат выполнения команды CQRS.
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// Показывает статус выполнененной команды.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Тип данных, опционально возвращаемых командой.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Опциональные данные, возвращаемые командой.
        /// </summary>
        public object Value { get; set; }
    }
}