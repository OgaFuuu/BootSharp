using BootSharp.Data.Interfaces;
using FluentNHibernate.Mapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        #region HasOne Helpers

        public void OneToZero<TTarget>(Expression<Func<T, TTarget>> navigationProperty, bool isNullable = false, IDataMap map = null)
            where TTarget : class, IDataObject
        {
            if (isNullable)
            {
                var relationship = References(navigationProperty).Nullable();
                if (map != null && map.KeysColumnNames != null)
                {
                    relationship.Columns(map.KeysColumnNames);
                }
            }
            else
            {
                var relationship = References(navigationProperty).Not.Nullable();
                if (map != null && map.KeysColumnNames != null)
                {
                    relationship.Columns(map.KeysColumnNames);
                }
            }
        }
        public void OneToOne<TTarget>(Expression<Func<T, TTarget>> navigationProperty, Expression<Func<TTarget, T>> inverseProperty, bool isNullable = false, IDataMap map = null)
             where TTarget : class, IDataObject
        {
            OneToZero(navigationProperty, isNullable, map);
        }
        public void OneToMany<TTarget>(Expression<Func<T, TTarget>> navigationProperty, Expression<Func<TTarget, ICollection<T>>> withManyProperty, bool isNullable = false, IDataMap map = null)
             where TTarget : class, IDataObject
        {
            OneToZero(navigationProperty, isNullable, map);
        }
        public void OneToMany<TTarget, TKey>(Expression<Func<T, TTarget>> navigationProperty, Expression<Func<TTarget, ICollection<T>>> withManyProperty, bool isNullable = false, Expression<Func<T, TKey>> foreignKeyProperty = null)
             where TTarget : class, IDataObject
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ManyToX Helpers

        public void ManyToOne<TTarget>(Expression<Func<T, IEnumerable<TTarget>>> navigationProperty, Expression<Func<TTarget, T>> inverseProperty, bool inverseIsNullable = false, IDataMap map = null)
            where TTarget : class, IDataObject
        {
            var relationship = HasMany(navigationProperty);
            if(map != null)
            {
                if (!string.IsNullOrEmpty(map.TableName))
                    relationship.Table(map.TableName);

                if (map.KeysColumnNames != null)
                    relationship.KeyColumns.Add(map.KeysColumnNames);
            }

        }
        public void ManyToMany<TTarget>(Expression<Func<T, IEnumerable<TTarget>>> navigationProperty, Expression<Func<TTarget, IEnumerable<T>>> inverseProperty, IDataMap map = null)
            where TTarget : class, IDataObject
        {
            var relationship = HasManyToMany(navigationProperty);
            if (map != null)
            {
                if (!string.IsNullOrEmpty(map.TableName))
                    relationship.Table(map.TableName);

                if (map.KeysColumnNames != null)
                    relationship.ParentKeyColumns.Add(map.KeysColumnNames);

                if (map.InverseKeysColumnNames != null)
                    relationship.ChildKeyColumns.Add(map.InverseKeysColumnNames);
            }
        }

        #endregion
    }
}
