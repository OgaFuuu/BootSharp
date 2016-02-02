using System.Collections.Generic;
using BootSharp.Data.Interfaces;
using BootSharp.DataImport.Interfaces;
using System.Linq;

namespace BootSharp.DataImport
{
    public class DataImportBase : IDataImport
    {
        public IDataContext SourceContext { get; private set; }
        public IDataContext DestinationContext { get; private set; }
        public IReadOnlyList<IDataImportJob> Jobs { get; private set; }

        public DataImportBase(IDataContext sourceContext, IDataContext destinationContext, IEnumerable<IDataImportJob> jobs)
        {
            SourceContext = sourceContext;
            DestinationContext = destinationContext;
            Jobs = jobs.ToList();
        }

        public IDictionary<IDataImportJob, IDataImportResult> Analyze()
        {
            var results = new Dictionary<IDataImportJob, IDataImportResult>();
            
            foreach(var job in Jobs)
            {
                var result = job.CanRun(SourceContext, DestinationContext);
                results.Add(job, result);
            }

            return results;
        }
        public IDictionary<IDataImportJob, IDataImportResult> Run()
        {
            var results = new Dictionary<IDataImportJob, IDataImportResult>();

            foreach (var job in Jobs)
            {
                var result = job.Run(SourceContext, DestinationContext);
                results.Add(job, result);
            }

            return results;
        }
    }
}
