using BootSharp.Data.Interfaces;
using System;

namespace BootSharp.Data
{
    public abstract class DataObjectBase : IDataObject
    {
        public virtual long Id { get; set; }
    }
}
