using System;

namespace Persons.Abstractions
{
    /// <summary>
    /// ���������� ������� CQRS.
    /// </summary>
    /// <typeparam name="TQuery">��� ������ ������� �������.</typeparam>
    /// <typeparam name="TResult">��� ������ ���������� �������.</typeparam>
    public interface IQueryHandler<TQuery, TResult>
    {
        /// <summary>
        /// ��������� ��������� �������.
        /// </summary>
        /// <param name="query">������ ������� ���� <typeparamref name="TQuery"/>.</param>
        /// <returns>��������� ������� � ���� ������� ������ <typeparamref name="TResult"/>.</returns>
        TResult Handle(TQuery query);
    }
}
