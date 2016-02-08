namespace BootSharp.Data.Interfaces
{
    /// <summary>
    /// Structure used to describes usual Mapping action.
    /// </summary>
    public interface IDataMap
    {
        string TableName { get; }

        string[] KeysColumnNames { get; }

        string[] InverseKeysColumnNames { get; }

        /// <summary>
        /// Setter for <see cref="TableName"/>.
        /// Fluent call.
        /// </summary>
        IDataMap Table(string tableName);


        /// <summary>
        /// Setter for <see cref="KeysColumnNames"/>.
        /// Fluent call.
        /// </summary>
        IDataMap Keys(params string[] keys);


        /// <summary>
        /// Setter for <see cref="InverseKeysColumnName"/>.
        /// Fluent call.
        /// </summary>
        IDataMap InverseKeys(params string[] keys);
    }
}
