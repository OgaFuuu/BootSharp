using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootSharp.Data.Interfaces
{
    /// <summary>
    /// Describes a persistency capable context.
    /// </summary>
    public interface IDataContext : IDisposable
    {
        /// <summary>
        /// Save all the changes that have been registered.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Sql query the database.
        /// </summary>
        IList<T> Query<T>(string sql, params object[] parameters);

        /// <summary>
        /// Async version of <see cref="Query{T}(string, object[])"/>.
        /// </summary>
        Task<IList<T>> QueryAsync<T>(string sql, params object[] parameters);

        /// <summary>
        /// Sql query the database.
        /// </summary>
        int Command(string sql, params object[] parameters);

        /// <summary>
        /// Async version of <see cref="Command{T}(string, object[])"/>.
        /// </summary>
        Task<int> CommandAsync(string sql, params object[] parameters);
    }
}
