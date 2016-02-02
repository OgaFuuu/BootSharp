using BootSharp.Data.Interfaces;

namespace BootSharp.Data.EntityFramework
{
    public class EfUnitOfWork : UnitOfWorkBase
    {
        private readonly EfDataContext _efDataContext;

        public EfUnitOfWork(EfDataContext efDataContext) : base(efDataContext)
        {
            _efDataContext = efDataContext;
        }

        protected override IDataRepository<T> CreateRepository<T>()
        {
            return new EfDataRepository<T>(_efDataContext);
        }
    }
}
