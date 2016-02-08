using BootSharp.Data;
using System.Collections.Generic;

namespace BootSharp.Tests.DataImport.Models
{
    public class Book : DataObjectBase
    {
        public virtual string Name { get; set; }

        public virtual Author Author { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual IList<Category> Categories { get; set; }
    }
}
