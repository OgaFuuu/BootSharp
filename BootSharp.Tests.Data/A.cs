using BootSharp.Data;
using System.Collections.Generic;

namespace BootSharp.Tests.Data
{
    public class A : DataObjectBase
    {
        public virtual string Name { get; set; }

        public virtual ICollection<B> BCollection { get; set; }

        public virtual ICollection<C> CCollection { get; set; }
    }
}
