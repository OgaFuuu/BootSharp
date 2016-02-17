using BootSharp.Data.Interfaces;
using BootSharp.DataImport.Interfaces;
using System;

namespace BootSharp.DataImport
{
    public abstract class DataImportJob : IDataImportJob
    {
        public virtual IDataImportResult CanRun(Func<IDataContext> sourceContextBuilder, Func<IDataContext> destinationContextBuilder)
        {
            return new DataImportResult();
        }

        public abstract IDataImportResult Run(Func<IDataContext> sourceContextBuilder, Func<IDataContext> destinationContextBuilder);
    }
}
