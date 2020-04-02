using System;

namespace Persons.Queries
{
    /// <summary>
    /// Запрос CQRS на извлечение единичной сущности <see cref="Persons.Entities.Person"/>.
    /// </summary>
    public class GetPersonQuery
    {
        /// <summary>
        /// ID сущности <see cref="Persons.Entities.Person"/>.
        /// </summary>
        public Guid PersonId { get; set; }

        public GetPersonQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
