using BootSharp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq.Expressions;

namespace BootSharp.Data.EntityFramework
{
    public class EfDataMapper<T> : EntityTypeConfiguration<T>, IDataMapper<T>
        where T : class, IDataObject
    {
        #region IDataMapper implementation

        public EfDataMapper() : base()
        {
            Map();
        }

        public void Map()
        {
            MapKeys();
            MapForeignKeys();
            MapProperties();
        }

        /// <summary>
        /// Map Primary Keys here using <see cref="EntityTypeConfiguration{TEntityType}.HasKey{TKey}"/>.
        /// Default implementation set <see cref="IDataObject.Id"/> of <see cref="T"/> has the only key.
        /// Do not call base.MapKeys() when using composite keys. (see https://msdn.microsoft.com/fr-fr/data/jj591617.aspx#1.2 for more info).
        /// </summary>
        protected virtual void MapKeys()
        {
            HasKey(e => e.Id);
        }

        /// <summary>
        /// Map Foreign Keys here using <see cref="EntityTypeConfiguration{TEntityType}.Property{TPrimitivePropertyConfiguration}(LambdaExpression)"/>.
        /// (see https://msdn.microsoft.com/en-us/data/jj591620.aspx for more info).
        /// </summary>
        protected virtual void MapForeignKeys()
        {
            // Placeholder
        }

        /// <summary>
        /// Map Properties here using <see cref="EntityTypeConfiguration{TEntityType}.Property{TPrimitivePropertyConfiguration}(LambdaExpression)"/>.
        /// Default implementation set <see cref="IDataObject.Id"/> of <see cref="T"/> with ColumnOrder 0 and <see cref="DatabaseGeneratedOption.Identity"/>
        /// </summary>
        protected virtual void MapProperties()
        {
            Property(e => e.Id).HasColumnOrder(0).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        #endregion

        #region Mapping helpers

        // TODO

        #endregion
    }
}
