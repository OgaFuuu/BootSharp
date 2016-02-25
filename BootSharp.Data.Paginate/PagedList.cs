using BootSharp.Data.Paginate.Interfaces;
using System;
using System.Collections.Generic;

namespace BootSharp.Data.Paginate
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public int PageCount { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        public PagedList(IEnumerable<T> elements, int pageCount, int pageNumber = 1, int pageSize = 50) : base(elements)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            if (pageCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageCount), "must be greater than 0.");

            PageNumber = pageNumber;
            PageSize = PageSize;
            PageCount = pageCount;
        }
    }
}
