using System;

namespace Persons.Abstractions
{
    /// <summary>
    /// Обработчик запроса CQRS.
    /// </summary>
    /// <typeparam name="TQuery">Тип данных объекта запроса.</typeparam>
    /// <typeparam name="TResult">Тип данных результата запроса.</typeparam>
    public interface IQueryHandler<TQuery, TResult>
    {
        /// <summary>
        /// Выполняет обработку запроса.
        /// </summary>
        /// <param name="query">Объект запроса типа <typeparamref name="TQuery"/>.</param>
        /// <returns>Результат запроса в виде объекта класса <typeparamref name="TResult"/>.</returns>
        TResult Handle(TQuery query);
    }
}
