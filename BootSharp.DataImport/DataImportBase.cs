using System.Collections.Generic;
using BootSharp.Data.Interfaces;
using BootSharp.DataImport.Interfaces;
using System.Linq;
using System;

namespace BootSharp.DataImport
{
    public class DataImportBase : IDataImport
    {
        public Func<IDataContext> SourceContextBuilder { get; private set; }
        public Func<IDataContext> DestinationContextBuilder { get; private set; }
        public IReadOnlyList<IDataImportJob> Jobs { get; private set; }

        public DataImportBase(Func<IDataContext> sourceContextBuilder, Func<IDataContext> destinationContextBuilder, IEnumerable<IDataImportJob> jobs)
        {
            SourceContextBuilder = sourceContextBuilder;
            DestinationContextBuilder = destinationContextBuilder;
            Jobs = jobs.ToList();
        }

        public IDictionary<IDataImportJob, IDataImportResult> Analyze()
        {
            var results = new Dictionary<IDataImportJob, IDataImportResult>();
            
            foreach(var job in Jobs)
            {
                var result = job.CanRun(SourceContextBuilder, DestinationContextBuilder);
                results.Add(job, result);
            }

            return results;
        }
        public IDictionary<IDataImportJob, IDataImportResult> Run()
        {
            var results = new Dictionary<IDataImportJob, IDataImportResult>();

            foreach (var job in Jobs)
            {
                var result = job.Run(SourceContextBuilder, DestinationContextBuilder);
                results.Add(job, result);
            }

            return results;
        }
    }
}
