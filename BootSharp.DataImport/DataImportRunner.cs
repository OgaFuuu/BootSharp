using BootSharp.DataImport.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BootSharp.DataImport
{
    public static class DataImportRunner
    {
        public static bool TryRun(IDataImport dataImport, out IDictionary<IDataImportJob, IDataImportResult> results)
        {
            var fail = false;
            results = null;

            try
            {
                results = Run(dataImport);
            }
            catch(Exception)
            {
                fail = true;
            }

            return !fail;
        }

        public static IDictionary<IDataImportJob, IDataImportResult> Run(IDataImport dataImport)
        {
            IDictionary<IDataImportJob, IDataImportResult> dico = null;

            try
            {
                // Check context first
                CheckContext(dataImport);

                // Analyze
                Analyze(dataImport);

                // Run
                dico = dataImport.Run();
            }
            catch(DataImportException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataImportException(dataImport, "An unknow error occured.", ex);
            }

            return dico;
        }

        /// <summary>
        /// Check that <see cref="IDataImport"/> context are properly set and reachable.
        /// Throw <see cref="DataImportException"/> if problems occurs.
        /// </summary>
        private static void CheckContext(IDataImport dataImport)
        {
            if (dataImport == null)
                throw new DataImportException(dataImport, "The importer can't be null", new ArgumentNullException(nameof(dataImport)));

            if (dataImport.SourceContextBuilder == null)
                throw new DataImportException(dataImport, "SourceContext can't be null.");

            if (dataImport.DestinationContextBuilder == null)
                throw new DataImportException(dataImport, "DestinationContext can't be null.");

            using (var sourceContext = dataImport.SourceContextBuilder.Invoke())
            {
                using (var sourceUow = sourceContext.CreateUnitOfWork())
                {
                    if (sourceUow == null)
                    {
                        throw new DataImportException(dataImport, "Unable to create an IUnitOfWork from SourceContext.");
                    }
                }
            }

            using (var destinationContext = dataImport.DestinationContextBuilder.Invoke())
            {
                using (var destUow = destinationContext.CreateUnitOfWork())
                {
                    if (destUow == null)
                    {
                        throw new DataImportException(dataImport, "Unable to create an IUnitOfWork from DestinationContext.");
                    }
                }
            }
        }

        /// <summary>
        /// Check that <see cref="IDataImport"/> can run properly.
        /// </summary>
        /// <param name="dataImport"></param>
        private static void Analyze(IDataImport dataImport)
        {
            var analysisFail = false;
            var analysisResults = dataImport.Analyze();
            var message = new StringBuilder("The data import job can't be run for the following reasons:");
            foreach (var result in analysisResults)
            {
                if(!result.Value.Succeed)
                {
                    analysisFail = true;
                    message.AppendLine(string.Format("Job: {0} - {1}", result.Key.ToString(), result.Value.Message));                    
                }
            }

            if(analysisFail)
                throw new DataImportException(dataImport, message.ToString());
        }
    }
}
