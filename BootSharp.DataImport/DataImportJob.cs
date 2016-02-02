using BootSharp.Data.Interfaces;
using BootSharp.DataImport.Interfaces;

namespace BootSharp.DataImport
{
    public abstract class DataImportJob : IDataImportJob
    {
        public virtual IDataImportResult CanRun(IDataContext sourceContext, IDataContext destinationContext)
        {
            return new DataImportResult();
        }

        public abstract IDataImportResult Run(IDataContext sourceContext, IDataContext destinationContext);
    }
}
