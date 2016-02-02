using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BootSharp.Data.Interfaces
{
    /// <summary>
    /// Exposing <see cref="IDataObject"/> CRUD/CQRS capabilities.
    /// ReadOnly version.
    /// </summary>
    public interface IDataReadOnlyRepository<T> : IDisposable
        where T : IDataObject
    {
        #region CRUD compliant
        
        /// <summary>
        /// Read <see cref="T"/> from the collection.
        /// Equivalent to SELECT statement.
        /// </summary>
        /// <param name="keyValues">Primary key values.</param>
        /// <returns>Unique matching object.</returns>
        T Read(params object[] keyValues);
        /// <summary>
        /// Read <see cref="IEnumerable{T}"/> from the collection.
        /// Equivalent to SELECT statement.
        /// </summary>
        /// <param name="filteringExpression">Optionnal <see cref="Expression{TDelegate}"/> used to filter elements.</param>
        /// <returns><see cref="IEnumerable{T}"/> of matching objects.</returns>
        IEnumerable<T> Read(Expression<Func<T, bool>> filteringExpression = null);
        
        #endregion

        #region CQRS compliant

        /// <summary>
        /// Query the persistency layer for readable elements.
        /// </summary>
        /// <param name="filteringExpression">Optionnal <see cref="Expression{TDelegate}"/> used to filter elements.</param>
        /// <param name="asDto">Optionnal <see cref="Boolean"/>. By default, <value>true</value> will expose DTO. Use <value>false</value> if you want object to be tracked.</param>
        /// <returns>An <see cref="IQueryable{T}"/> correctly filtered.</returns>
        IQueryable<T> Query(Expression<Func<T, bool>> filteringExpression = null, bool asDto = false);

        #endregion
    }
}
