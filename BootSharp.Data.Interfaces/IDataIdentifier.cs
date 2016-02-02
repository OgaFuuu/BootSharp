namespace BootSharp.Data.Interfaces
{
    /// <summary>
    /// Special object used as an external Id.
    /// </summary>
    public interface IDataIdentifier<TEntity, TIdentifier> : IDataObject
        where TEntity : IDataObject
    {
        /// <summary>
        /// Object on which this identifier apply.
        /// </summary>
        TEntity Source { get; set; }

        /// <summary>
        /// External system unique identifier.
        /// </summary>
        TIdentifier IdentifierType { get; set; }

        string Value { get; set; }
    }
}
