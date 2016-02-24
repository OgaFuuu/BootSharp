using System;

namespace BootSharp.Business.Interfaces
{
    /// <summary>
    /// Base interface describing a service.
    /// </summary>
    public interface IService : IDisposable
    {
        /// <summary>
        /// The service name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The service description.
        /// </summary>
        string Description { get; }
    }
}
