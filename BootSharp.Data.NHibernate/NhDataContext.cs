using BootSharp.Data.Interfaces;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootSharp.Data.NHibernate
{
    public abstract class NhDataContext : IDataContext
    {
        private IInterceptor _localInterceptor { get; set; }
        private AutoPersistenceModel _autoPersistanceModel { get; set; }

        internal ISessionFactory Factory { get; private set; }
        internal ISession Session { get; private set; }

        private NhDataContext(IInterceptor sessionLocalInterceptor, AutoPersistenceModel autoPersistanceModel = null)
        {
            _localInterceptor = sessionLocalInterceptor;
            _autoPersistanceModel = autoPersistanceModel;
        }
        public NhDataContext(IPersistenceConfigurer dbPersister, IInterceptor sessionLocalInterceptor = null, AutoPersistenceModel autoPersistanceModel = null) : this(sessionLocalInterceptor, autoPersistanceModel)
        {
            Factory = NhHelper.GetSessionFactory(this, dbPersister, _autoPersistanceModel);
            ConfigureSession();
        }
        public NhDataContext(FluentConfiguration factoryConfig, IInterceptor sessionLocalInterceptor = null, AutoPersistenceModel autoPersistanceModel = null) : this(sessionLocalInterceptor, autoPersistanceModel)
        {
            Factory = NhHelper.GetSessionFactory(this, factoryConfig, _autoPersistanceModel);
            ConfigureSession();
        }

        internal void ConfigureSession()
        {
            Session = _localInterceptor != null ? Factory.OpenSession(_localInterceptor) : Factory.OpenSession();

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
