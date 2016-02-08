using BootSharp.Data.Interfaces;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootSharp.Data.NHibernate
{
    public abstract class NhDataContext : IDataContext
    {
        internal readonly ISession Session;

        public NhDataContext(IPersistenceConfigurer dbPersister, IInterceptor sessionLocalInterceptor = null, AutoPersistenceModel autoPersistanceModel = null)
        {
            var factory = NhHelper.GetSessionFactory(this, dbPersister, autoPersistanceModel);
            Session = sessionLocalInterceptor != null ? factory.OpenSession(sessionLocalInterceptor) : factory.OpenSession();

            // Data Context IS transactionnal so...
            Session.BeginTransaction();
        }

        public void SaveChanges()
        {
            Session.Flush();
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            return new NhUnitOfWork(this);
        }

        public IList<T> Query<T>(string sql, params object[] parameters)
        {
            var listTask = QueryAsync<T>(sql, parameters);
            Task.WaitAll(listTask);

            return listTask.Result;
        }

        public async Task<IList<T>> QueryAsync<T>(string sql, params object[] parameters)
        {
            var listTask = new Task<IList<T>>(() => Session.CreateSQLQuery(sql).List<T>());
            listTask.Start();

            return await listTask;
        }

        public int Command(string sql, params object[] parameters)
        {
            var listTask = CommandAsync(sql, parameters);
            Task.WaitAll(listTask);

            return listTask.Result;
        }

        public async Task<int> CommandAsync(string sql, params object[] parameters)
        {
            var commandTask = new Task<int>(() => Session.CreateSQLQuery(sql).ExecuteUpdate());
            commandTask.Start();

            return await commandTask;
        }

        public virtual void Dispose()
        {
            if(Session != null)
            {
                Session.Dispose();
            }
        }
    }
}
