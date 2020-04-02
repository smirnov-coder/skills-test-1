using Persons.Entities;
using System;

namespace Persons.Repositories
{
    /// <summary>
    /// ����������� ��������� <see cref="Person"/>.
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// ��������� �� ����������� �������� <see cref="Person"/> �� ID.
        /// </summary>
        /// <param name="id">ID ����������� ��������.</param>
        /// <returns>������ �������� <see cref="Person"/> ���� null, ���� �������� �� �������.</returns>
        Person Find(Guid id);

        /// <summary>
        /// ��������� � ����������� �������� <see cref="Person"/>.
        /// </summary>
        /// <param name="item">����������� �������� <see cref="Person"/>.</param>
        void Insert(Person item);
    }
}
