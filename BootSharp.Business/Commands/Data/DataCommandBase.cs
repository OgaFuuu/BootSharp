using BootSharp.Data.Interfaces;
using System;

namespace BootSharp.Business.Commands.Data
{
    public abstract class DataCommandBase<T> : CommandBase<T>
    {
        protected IUnitOfWork _uow { get; private set; }

        protected DataCommandBase(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException(nameof(uow));

            _uow = uow;
        }
    }
}
