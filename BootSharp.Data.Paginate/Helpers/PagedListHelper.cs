using BootSharp.Data.Interfaces;
using BootSharp.Data.Paginate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BootSharp.Data.Paginate.Helpers
{
    public static class PagedListHelper
    {
        public static IPagedList<T> ReadPagedList<T>(this IDataRepository<T> repository, Expression<Func<T, bool>> filteringExpression = null, int pageNumber = 1, int pageSize = 50)
            where T : IDataObject
        {
            // Check arguments
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "should be greater than 0.");

            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "should be greater than 0.");

            // Get Collection size
            var query = repository.Query(filteringExpression);
            return ToPagedList(query, pageNumber, pageSize);
        }
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> collection, int pageNumber = 1, int pageSize = 50) 
            where T : IDataObject
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            var collectionSize = collection.Count();

            // Check pageCount
            var pageCount = (collectionSize / pageSize) + (collectionSize % pageSize > 0 ? 1 : 0);
            if (pageNumber > (pageCount + 1))
            {
                var message = string.Format("Exceed the total number of page ({0})", pageCount);
                throw new ArgumentOutOfRangeException(nameof(pageNumber), message);
            }

            // Check skip
            var toSkip = (pageNumber - 1) * pageSize;
            var elements = collection.Skip(toSkip).Take(pageSize).ToList();

            return new PagedList<T>(elements, pageCount, pageNumber, pageSize);
        }
    }
}
