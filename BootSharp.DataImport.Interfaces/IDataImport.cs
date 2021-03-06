﻿using BootSharp.Data.Interfaces;
using System.Collections.Generic;

namespace BootSharp.DataImport.Interfaces
{
    /// <summary>
    /// Describes an data import contract.
    /// </summary>
    public interface IDataImport
    {
        /// <summary>
        /// Source context.
        /// </summary>
        IDataContext SourceContext { get; }

        /// <summary>
        /// Destination context.
        /// </summary>
        IDataContext DestinationContext { get; }

        /// <summary>
        /// Ordered list of Job played by the importer.
        /// </summary>
        IReadOnlyList<IDataImportJob> Jobs { get; }

        /// <summary>
        /// Give analysis results for all jobs.
        /// </summary>
        /// <returns></returns>
        IDictionary<IDataImportJob, IDataImportResult> Analyze();

        /// <summary>
        /// Run all jobs.
        /// </summary>
        /// <returns></returns>
        IDictionary<IDataImportJob, IDataImportResult> Run();
    }
}
