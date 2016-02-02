using BootSharp.Data.Interfaces;

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
        IDataImportResult CanRun(IDataContext sourceContext, IDataContext destinationContext);

        /// <summary>
        /// Run the job.
        /// </summary>
        /// <returns></returns>
        IDataImportResult Run(IDataContext sourceContext, IDataContext destinationContext);
    }
}
