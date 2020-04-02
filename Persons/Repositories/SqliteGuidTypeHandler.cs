using Dapper;
using System;
using System.Data;

namespace Persons.Repositories
{
    /// <summary>
    /// Кастомный маппинг Guid-значений (встроенный почему-то работает из рук вон плохо, извините). :(
    /// </summary>
    public class SqliteGuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override void SetValue(IDbDataParameter parameter, Guid guid)
        {
            parameter.Value = guid.ToString();
        }

        public override Guid Parse(object value)
        {
            return Guid.Parse((string)value);
        }
    }
}
