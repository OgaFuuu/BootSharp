using BootSharp.Data;
using System.Collections.Generic;

namespace BootSharp.Tests.DataImport.Models
{
    public class Author : DataObjectBase
    {
        public virtual string Name { get; set; }

        public virtual IList<Book> Books { get; set; }
    }
}
