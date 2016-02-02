using BootSharp.Data.Interfaces;

namespace BootSharp.Data
{
    public abstract class DataIdentifierBase<TEntity, TIdentifier> : DataObjectBase, IDataIdentifier<TEntity, TIdentifier>
        where TEntity : IDataObject
    {
        public virtual TIdentifier IdentifierType { get; set; }
        public virtual TEntity Source { get; set; }
        public virtual string Value { get; set; }
    }
}
