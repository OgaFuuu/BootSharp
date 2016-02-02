using BootSharp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BootSharp.Data.EntityFramework
{
    public class EfDataRepository<T> : DataRepositoryBase<T> where T : class, IDataObject
    {
        private readonly EfDataContext _efDataContext;
        public EfDataRepository(EfDataContext efDataContext) : base(efDataContext)
        {
            _efDataContext = efDataContext;
        }

        #region CRUD

        public override void Create(T entity)
        {
            var set = _efDataContext.Set<T>();
            set.Add(entity);
        }
        public override void Create(IEnumerable<T> entities)
        {
            var set = _efDataContext.Set<T>();
            set.AddRange(entities);
        }
        public override T Read(params object[] keyValues)
        {
            var set = _efDataContext.Set<T>();
            return set.Find(keyValues);
        }
        public override IEnumerable<T> Read(Expression<Func<T, bool>> filteringExpression = null)
        {
            return Query(filteringExpression).ToList();
        }
        public override void Update(T entity)
        {
            var state = _efDataContext.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                var set = _efDataContext.Set<T>();
                set.Attach(entity);
                _efDataContext.Entry(entity).State = EntityState.Modified;
            }
        }
        public override void Update(IEnumerable<T> entities)
        {
            var set = _efDataContext.Set<T>();

            foreach (var entity in entities)
            {
                var state = _efDataContext.Entry(entity).State;
                if (state == EntityState.Detached)
                {
                    set.Attach(entity);
                    _efDataContext.Entry(entity).State = EntityState.Modified;
                }
            }

        }
        public override void Delete(T entity)
        {
            var set = _efDataContext.Set<T>();
            var state = _efDataContext.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                set.Attach(entity);
            }

            set.Remove(entity);
        }
        public override void Delete(IEnumerable<T> entities)
        {
            var set = _efDataContext.Set<T>();

            var list = entities.ToList();
            foreach (var entity in list)
            {
                var state = _efDataContext.Entry(entity).State;
                if (state == EntityState.Detached)
                {
                    set.Attach(entity);
                }
            }

            set.RemoveRange(list);
        }

        #endregion
        
        #region CQRS

        public override IQueryable<T> Query(Expression<Func<T, bool>> filteringExpression = null, bool asDto = false)
        {
            var set = _efDataContext.Set<T>();
            var result = set.AsQueryable();

            if (filteringExpression != null)
            {
                result = result.Where(filteringExpression);
            }

            return asDto ? result.AsNoTracking() : result;
        }      

        #endregion
    }
}
