using System;

namespace BootSharp.Data.Interfaces
{
    /// <summary>
    /// Unit of work pattern descriptor.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Get the <see cref="IDataRepository{T}"/> associated with <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="IDataObject"/> implementation.</typeparam>
        /// <returns><see cref="IDataRepository{T}"/> found.</returns>
        IDataRepository<T> GetRepository<T>() where T : class, IDataObject;

        /// <summary>
        /// Save every changes.
        /// </summary>
        void Save();
    }
}
