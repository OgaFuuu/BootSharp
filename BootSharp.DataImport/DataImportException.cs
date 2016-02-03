using BootSharp.DataImport.Interfaces;
using System;

namespace BootSharp.DataImport
{
    public class DataImportException : Exception
    {
        public IDataImport DataImport { get; private set; }
        public DateTime Occured { get; private set; }

        public DataImportException(IDataImport dataImport, string message, Exception innerException = null) : base(message, innerException)
        {
            Occured = DateTime.Now;
            DataImport = dataImport;
        }
    }
}
