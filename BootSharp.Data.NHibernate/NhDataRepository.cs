using BootSharp.Data.Interfaces;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BootSharp.Data.NHibernate
{
    public class NhDataRepository<T> : DataRepositoryBase<T> where T : class, IDataObject
    {
        private readonly NhDataContext _nhDataContext;

        protected ISession Session { get { return _nhDataContext.Session; } }
         
        public NhDataRepository(NhDataContext nhDataContext) : base(nhDataContext)
        {
            _nhDataContext = nhDataContext;
        }

        #region CRUD

        public override void Create(T entity)
        {
            var set = Session.Save(entity);
        }
        public override void Create(IEnumerable<T> entities)
        {
            foreach(var e in entities)
            {
                Create(e);
            }
        }
        public override T Read(params object[] keyValues)
        {
            return Session.Get<T>(keyValues);
        }
        public override IEnumerable<T> Read(Expression<Func<T, bool>> filteringExpression = null)
        {
            return Query(filteringExpression).ToList();
        }
        public override void Update(T entity)
        {
            Session.Update(entity);
        }
        public override void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }
        public override void Delete(T entity)
        {
            Session.Delete(entity);
        }
        public override void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        #endregion

        #region CQRS

        public override IQueryable<T> Query(Expression<Func<T, bool>> filteringExpression = null, bool asDto = false)
        {
            var result = Session.Query<T>();

            if (filteringExpression != null)
            {
                result = result.Where(filteringExpression);
            }

            return result;
        }

        #endregion
    }
}
