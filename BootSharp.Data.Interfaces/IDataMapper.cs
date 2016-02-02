namespace BootSharp.Data.Interfaces
{
    /// <summary>
    /// Base interface used for fluent mapping.
    /// </summary>
    public interface IDataMapper
    {
        /// <summary>
        /// Execute the mapping logic.
        /// </summary>
        void Map();
    } 

    /// <summary>
    /// Generic version of <see cref="IDataMapper"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataMapper<T> : IDataMapper
        where T : IDataObject
    {
    }
}
