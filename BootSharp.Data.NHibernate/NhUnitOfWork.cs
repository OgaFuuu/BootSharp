using BootSharp.Data.Interfaces;
using NHibernate;
using System;

namespace BootSharp.Data.NHibernate
{
    public class NhUnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        private readonly NhDataContext _nhContext;

        private ITransaction _transaction;

        public NhUnitOfWork(NhDataContext nhContext) : base(nhContext)
        {
            _nhContext = nhContext;

            if (_nhContext.Session == null || !_nhContext.Session.IsOpen)
                _nhContext.ConfigureSession();

            _transaction = _nhContext.Session.BeginTransaction();
        }

        protected override IDataRepository<T> CreateRepository<T>()
        {
            return new NhDataRepository<T>(_nhContext);
        }

        public override void Save()
        {
            try
            {
                _transaction.Commit();
                base.Save();
            }
            catch(Exception ex)
            {
                if (_transaction == null)
                    throw new Exception("The transaction was not set.", ex);

                if (_transaction.WasCommitted)
                {
                    throw new Exception("An error occured. The transaction was already commited.", ex);
                }
                else if (_transaction.WasRolledBack)
                {
                    throw new Exception("An error occured. The transaction was rolled back.", ex);
                }

                _transaction.Rollback();
                throw;
            }
            finally
            {
                if (_transaction != null)
                    _transaction.Dispose();

                _transaction = _nhContext.Session.BeginTransaction();
            }

        }

        public override void Dispose()
        {
            if(_transaction != null)
            {
                if (!_transaction.WasCommitted)
                    _transaction.Rollback();

                _transaction.Dispose();
            }

            base.Dispose();
        }
    }
}
