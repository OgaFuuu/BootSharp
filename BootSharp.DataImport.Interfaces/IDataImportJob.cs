using BootSharp.Data.Interfaces;
using System;

namespace BootSharp.DataImport.Interfaces
{
    /// <summary>
    /// Describes a importation Job.
    /// </summary>
    public interface IDataImportJob
    {
        /// <summary>
        /// Check that the job can run.
        /// </summary>
        /// <returns></returns>
        IDataImportResult CanRun(Func<IDataContext> sourceContextBuilder, Func<IDataContext> destinationContextBuilder);

        /// <summary>
        /// Run the job.
        /// </summary>
        /// <returns></returns>
        IDataImportResult Run(Func<IDataContext> sourceContextBuilder, Func<IDataContext> destinationContextBuilder);
    }
}
