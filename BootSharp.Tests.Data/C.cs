using BootSharp.Data;
using System.Collections.Generic;

namespace BootSharp.Tests.Data
{
    public class C : DataObjectBase
    {
        public virtual string Name { get; set; }

        public virtual IList<A> ACollection { get; set; }
    }
}
