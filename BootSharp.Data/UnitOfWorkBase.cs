using BootSharp.Data.Interfaces;
using System;
using System.Collections.Concurrent;

namespace BootSharp.Data
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        private readonly IDataContext _dataContext;
        private readonly ConcurrentDictionary<Type, object> _repositories;

        protected UnitOfWorkBase(IDataContext dataContext)
        {
            _dataContext = dataContext;
            _repositories = new ConcurrentDictionary<Type, object>();
        }

        public IDataRepository<T> GetRepository<T>() where T : class, IDataObject
        {
            var repositoryType = typeof(T);
            var repo = _repositories.GetOrAdd(repositoryType, CreateRepository<T>());
            return repo as IDataRepository<T>;
        }

        /// <summary>
        /// Provides a new instance of <see cref="IDataRepository{T}"/>
        /// </summary>
        protected abstract IDataRepository<T> CreateRepository<T>() where T : class, IDataObject;

        public virtual void Save()
        {
            _dataContext.SaveChanges();
        }

        public virtual void Dispose()
        {
        }
    }
}
