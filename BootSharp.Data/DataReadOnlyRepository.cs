using BootSharp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BootSharp.Data
{
    public abstract class DataReadOnlyRepository<T> : IDataReadOnlyRepository<T>
        where T : IDataObject
    {
        public abstract T Read(params object[] keyValues);
        public abstract IEnumerable<T> Read(Expression<Func<T, bool>> filteringExpression = null);

        public abstract IQueryable<T> Query(Expression<Func<T, bool>> filteringExpression = null, bool asDto = false);

        public virtual void Dispose()
        {
        }
    }
}
