using System.Collections.Generic;

namespace BootSharp.Data.Interfaces
{
    /// <summary>
    /// Exposing <see cref="IDataObject"/> CRUD/CQRS capabilities.
    /// </summary>
    public interface IDataRepository<T> : IDataReadOnlyRepository<T>
        where T : IDataObject
    {
        #region CRUD compliant

        /// <summary>
        /// Add <see cref="T"/> to the collection.
        /// Equivalent to INSERT statement.
        /// </summary>
        /// <param name="entity">The <see cref="IDataObject"/> to add.</param>
        void Create(T entity);
        /// <summary>
        /// Add an <see cref="IEnumerable{T}"/> to the collection.
        /// Equivalent to INSERT statement.
        /// </summary>
        /// <param name="entities">The <see cref="IEnumerable{T}"/> of <see cref="IDataObject"/> to add.</param>
        void Create(IEnumerable<T> entities);
        
        /// <summary>
        /// Update <see cref="T"/> in the collection.
        /// Equivalent to UPDATE statement.
        /// </summary>
        /// <param name="entity">The <see cref="IDataObject"/> to update.</param>
        void Update(T entity);
        /// <summary>
        /// Update an <see cref="IEnumerable{T}"/> in the collection.
        /// Equivalent to UPDATE statement.
        /// </summary>
        /// <param name="entities">The <see cref="IEnumerable{T}"/> of <see cref="IDataObject"/> to update.</param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// Delete <see cref="T"/> from the collection.
        /// Equivalent to DELETE statement.
        /// </summary>
        /// <param name="entity">The <see cref="IDataObject"/> to delete.</param>
        void Delete(T entity);
        /// <summary>
        /// Delete an <see cref="IEnumerable{T}"/> from the collection.
        /// Equivalent to DELETE statement.
        /// </summary>
        /// <param name="entities">The <see cref="IEnumerable{T}"/> of <see cref="IDataObject"/> to delete.</param>
        void Delete(IEnumerable<T> entities);

        #endregion
    }
}
