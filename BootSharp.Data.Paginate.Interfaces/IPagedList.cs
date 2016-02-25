using System.Collections.Generic;

namespace BootSharp.Data.Paginate.Interfaces
{
    public interface IPagedList<T> : IList<T>
    {
        /// <summary>
        /// Total number of page available in the original List.
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// PageSize used to generate this Paged List.
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// The page number this paged list corresponds to in the original collection.
        /// </summary>
        int PageNumber { get; }
    }
}
