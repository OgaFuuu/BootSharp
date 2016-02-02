using BootSharp.Data.Interfaces;
using FluentNHibernate.Mapping;

namespace BootSharp.Data.NHibernate
{
    public abstract class NhDataMapper<T> : ClassMap<T>, IDataMapper<T>
        where T : IDataObject
    {
        #region IDataMapper implementation

        protected NhDataMapper() : this(null)
        {
        }

        protected NhDataMapper(string tableName = null)
        {
            // Map table name
            if (string.IsNullOrWhiteSpace(tableName))
            {
                var type = typeof(T);
                tableName = type.Name;
            }

            Table(tableName);
            Map();
        }

        public void Map()
        {
            MapKeys();
            MapForeignKeys();
            MapProperties();
        }

        /// <summary>
        /// Map Primary Keys here.
        /// Default implementation set <see cref="IDataObject.Id"/> of <see cref="T"/> has the only key.
        /// Do not call base.MapKeys() when using composite keys.
        /// </summary>
        protected virtual void MapKeys()
        {
            Id(x => x.Id)
                .GeneratedBy
                .Identity();
        }

        /// <summary>
        /// Map Foreign Keys here.
        /// </summary>
        protected virtual void MapForeignKeys()
        {
            // Placeholder
        }

        /// <summary>
        /// Map Properties here using.
        /// </summary>
        protected virtual void MapProperties()
        {
        }

        #endregion

        #region IDataMapper implementation

        // TODO

        #endregion
    }
}
