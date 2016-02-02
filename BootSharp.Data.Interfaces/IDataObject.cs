using System;

namespace BootSharp.Data.Interfaces
{
    // <summary>
    /// Indicates compatibility with an <see cref="IDataContext"/> object.
    /// </summary>
    public interface IDataObject
    {
        /// <summary>
        /// Technical Identifier
        /// </summary>
        long Id { get; set; }
    }
}
