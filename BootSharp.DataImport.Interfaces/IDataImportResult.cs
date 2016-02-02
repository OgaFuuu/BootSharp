using System.Collections.Generic;

namespace BootSharp.DataImport.Interfaces
{
    /// <summary>
    /// Contract for data import results.
    /// </summary>
    public interface IDataImportResult
    {
        /// <summary>
        /// Data import global result state.
        /// </summary>
        bool Succeed { get; }

        /// <summary>
        /// Description of the result.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Results of import.
        /// </summary>
        IDictionary<object, object> Results { get; }
    }
}
