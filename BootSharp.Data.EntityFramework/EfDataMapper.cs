using BootSharp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
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

        #region OneToX Helpers
        
        public void OneToZero<TTarget>(Expression<Func<T, TTarget>> navigationProperty, bool isNullable = false, IDataMap map = null) 
            where TTarget : class, IDataObject
        {
            if (isNullable)
            {
                HasOptional(navigationProperty);
            }
            else
            {
                HasRequired(navigationProperty);
            }
        }        
        public void OneToOne<TTarget>(Expression<Func<T, TTarget>> navigationProperty, Expression<Func<TTarget, T>> inverseProperty, bool isNullable = false, IDataMap map = null)
             where TTarget : class, IDataObject
        {
            if (isNullable)
            {
                var relationship = HasOptional(navigationProperty).WithRequired(inverseProperty); // TODO take optionnalPrincipal and optionnalDependant into account.
                if(map != null)
                {
                    relationship.Map(m =>
                    {
                        if (map.TableName != null)
                            m.ToTable(map.TableName);

                        if (map.KeysColumnNames != null)
                            m.MapKey(map.KeysColumnNames);
                    });
                }
            }
            else
            {
                var relationship = HasRequired(navigationProperty).WithOptional(inverseProperty); // TODO take requiredPrincipal and requiredDependant into account.
                if(map != null)
                {
                    relationship.Map(m =>
                    {
                        if (map.TableName != null)
                            m.ToTable(map.TableName);

                        if (map.KeysColumnNames != null)
                            m.MapKey(map.KeysColumnNames);
                    });
                }
            }
        }       
        public void OneToMany<TTarget>(Expression<Func<T, TTarget>> navigationProperty, Expression<Func<TTarget, ICollection<T>>> withManyProperty, bool isNullable = false, IDataMap map = null)
             where TTarget : class, IDataObject
        {
            if (isNullable)
            {
                var relationship = HasOptional(navigationProperty).WithMany(withManyProperty);
                if (map != null)
                {
                    relationship.Map(m =>
                    {
                        if (map.TableName != null)
                            m.ToTable(map.TableName);

                        if (map.KeysColumnNames != null)
                            m.MapKey(map.KeysColumnNames);
                    });
                }
            }
            else
            {
                var relationship = HasRequired(navigationProperty).WithMany(withManyProperty); // TODO take requiredPrincipal and requiredDependant into account.
                if (map != null)
                {
                    relationship.Map(m =>
                    {
                        if (map.TableName != null)
                            m.ToTable(map.TableName);

                        if (map.KeysColumnNames != null)
                            m.MapKey(map.KeysColumnNames);
                    });
                }
            }
        }
        public void OneToMany<TTarget, TKey>(Expression<Func<T, TTarget>> navigationProperty, Expression<Func<TTarget, ICollection<T>>> withManyProperty, bool isNullable = false, Expression<Func<T, TKey>> foreignKeyProperty = null)
             where TTarget : class, IDataObject
        {
            if (isNullable)
            {
                var relationship = HasOptional(navigationProperty).WithMany(withManyProperty);
                if (foreignKeyProperty != null)
                {
                    relationship.HasForeignKey(foreignKeyProperty);
                }
            }
            else
            {
                var relationship = HasRequired(navigationProperty).WithMany(withManyProperty);
                if (foreignKeyProperty != null)
                {
                    relationship.HasForeignKey(foreignKeyProperty);
                }
            }
        }

        #endregion

        #region ManyToX Helpers

        public void ManyToOne<TTarget>(Expression<Func<T, ICollection<TTarget>>> navigationProperty, Expression<Func<TTarget, T>> inverseProperty, bool inverseIsNullable = false, IDataMap map = null)
            where TTarget : class, IDataObject
        {
            DependentNavigationPropertyConfiguration<TTarget> relationship = null;
            if(inverseIsNullable)
            {
                relationship = HasMany(navigationProperty).WithOptional(inverseProperty); //TODO take withOptionnal into account
            }
            else
            {
                relationship = HasMany(navigationProperty).WithRequired(inverseProperty); //TODO take withOptionnal into account
            }
            
            if (map != null)
            {
                relationship.Map(m =>
                {
                    if (!string.IsNullOrEmpty(map.TableName))
                        m.ToTable(map.TableName);

                    if (map.KeysColumnNames != null)
                        m.MapKey(map.KeysColumnNames);
                });
            }
        }
        public void ManyToMany<TTarget>(Expression<Func<T, ICollection<TTarget>>> navigationProperty, Expression<Func<TTarget, ICollection<T>>> inverseProperty, IDataMap map = null)
            where TTarget : class, IDataObject
        {
            var relationship = HasMany(navigationProperty).WithMany(inverseProperty);

            if (map != null)
            {
                relationship.Map(m =>
                    {
                        if(!string.IsNullOrEmpty(map.TableName))
                            m.ToTable(map.TableName);

                        if (map.KeysColumnNames != null)
                            m.MapLeftKey(map.KeysColumnNames);

                        if (map.InverseKeysColumnNames != null)
                            m.MapRightKey(map.InverseKeysColumnNames);
                    });
            }
        }

        #endregion
    }
}
