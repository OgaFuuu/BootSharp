using BootSharp.Data.Interfaces;

namespace BootSharp.Business.Interfaces
{
    /// <summary>
    /// Base contract for data service.
    /// </summary>
    public interface IDataService : IService
    {
        /// <summary>
        /// <see cref="IUnitOfWork"/> used by the service.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
