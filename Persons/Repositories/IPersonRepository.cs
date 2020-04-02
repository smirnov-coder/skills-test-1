using Persons.Entities;
using System;

namespace Persons.Repositories
{
    /// <summary>
    /// Репозиторий сущностей <see cref="Person"/>.
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Извлекает из репозитория сущность <see cref="Person"/> по ID.
        /// </summary>
        /// <param name="id">ID извлекаемой сущности.</param>
        /// <returns>Объект сущности <see cref="Person"/> либо null, если сущность не найдена.</returns>
        Person Find(Guid id);

        /// <summary>
        /// Добавляет в репозиторий сущность <see cref="Person"/>.
        /// </summary>
        /// <param name="item">Добавляемая сущность <see cref="Person"/>.</param>
        void Insert(Person item);
    }
}
