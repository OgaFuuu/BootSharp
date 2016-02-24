using BootSharp.Business.Interfaces;
using BootSharp.Data.Interfaces;
using System;

namespace BootSharp.Business
{
    public abstract class DataServiceBase : ServiceBase, IDataService
    {
        private IUnitOfWork _unitOfWork;

        protected DataServiceBase(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public virtual IUnitOfWork UnitOfWork { get { return _unitOfWork;} }
    }
}
