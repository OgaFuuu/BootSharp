using BootSharp.Data.Interfaces;
using System.Collections.Generic;

namespace BootSharp.Data
{
    public abstract class DataRepositoryBase<T> : DataReadOnlyRepository<T>, IDataRepository<T> 
        where T : IDataObject
    {
        private readonly IDataContext _dataContext;

        protected DataRepositoryBase(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected IDataContext DataContext
        {
            get { return _dataContext; }
        }

        public abstract void Create(T entity);
        public abstract void Create(IEnumerable<T> entities);
        public abstract void Update(T entity);
        public abstract void Update(IEnumerable<T> entities);
        public abstract void Delete(T entity);
        public abstract void Delete(IEnumerable<T> entities);
        
    }
}
