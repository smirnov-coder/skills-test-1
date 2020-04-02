using System;

namespace Persons.Queries
{
    /// <summary>
    /// ������ CQRS �� ���������� ��������� �������� <see cref="Persons.Entities.Person"/>.
    /// </summary>
    public class GetPersonQuery
    {
        /// <summary>
        /// ID �������� <see cref="Persons.Entities.Person"/>.
        /// </summary>
        public Guid PersonId { get; set; }

        public GetPersonQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
