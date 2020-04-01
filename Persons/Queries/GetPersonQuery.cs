using System;

namespace Persons.Queries
{
    public class GetPersonQuery
    {
        public Guid PersonId { get; set; }

        public GetPersonQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
