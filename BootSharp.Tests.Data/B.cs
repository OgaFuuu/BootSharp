using BootSharp.Data;

namespace BootSharp.Tests.Data
{
    public class B : DataObjectBase
    {
        public virtual string Name { get; set; }

        public virtual A A { get; set; }
    }
}
