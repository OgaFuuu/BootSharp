using BootSharp.Data.Interfaces;

namespace BootSharp.Data
{
    public sealed class DataMapBase : IDataMap
    {
        public string TableName { get; private set; }
        public string[] KeysColumnNames { get; private set; }
        public string[] InverseKeysColumnNames { get; private set; }

        public DataMapBase(string tableName)
        {
            Table(tableName);
        }

        public DataMapBase(string tableName, params string[] keysColumnName) : this(tableName)
        {
            Keys(keysColumnName);
        }

        public DataMapBase(string tableName, string[] keysColumnName = null, string[] inverseKeysColumnName = null) : this(tableName, keysColumnName)
        {
            InverseKeys(inverseKeysColumnName);
        }

        public DataMapBase(string tableName, string keyColumnName = null, string inverseKeyColumnName = null) : this(tableName)
        {
            Keys(keyColumnName);
            InverseKeys(inverseKeyColumnName);
        }

        /// <summary>
        /// Setter for <see cref="TableName"/>.
        /// Fluent call.
        /// </summary>
        public IDataMap Table(string tableName)
        {
            TableName = tableName;

            return this;
        }

        /// <summary>
        /// Setter for <see cref="KeysColumnNames"/>.
        /// Fluent call.
        /// </summary>
        public IDataMap Keys(params string[] keys)
        {
            KeysColumnNames = keys;

            return this;
        }

        /// <summary>
        /// Setter for <see cref="InverseKeysColumnName"/>.
        /// Fluent call.
        /// </summary>
        public IDataMap InverseKeys(params string[] keys)
        {
            InverseKeysColumnNames = keys;

            return this;
        }
    }
}
