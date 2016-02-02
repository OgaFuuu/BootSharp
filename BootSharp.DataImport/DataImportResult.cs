using System.Collections.Generic;
using BootSharp.DataImport.Interfaces;

namespace BootSharp.DataImport
{
    public class DataImportResult : IDataImportResult
    {
        public bool Succeed { get; protected set; }
        public string Message { get; protected set; }
        public IDictionary<object, object> Results { get; protected set; }

        public DataImportResult(bool succeed = true, string message = null, IDictionary<object, object> results = null)
        {
            Succeed = succeed;
            Message = message;
            Results = results;
        }
    }
}
