using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
        #region OneToX Helpers

        /// <summary>
        /// Maps a one-to-zero relationship.
        /// Also called "Reference" or "HasOne" relationship.
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="isNullable">Indicates wether or not <see cref="navigationProperty"/> is nullable in the database.</param>
        void OneToZero<TTarget>(Expression<Func<T, TTarget>> navigationProperty, bool isNullable = false, IDataMap map = null)
            where TTarget : class, IDataObject;

        /// <summary>
        /// Maps a one-to-one relationship.
        /// Also called "Reference" or "HasOne" relationship.
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="isNullable">Indicates wether or not <see cref="navigationProperty"/> is nullable in the database.</param>
        /// <param name="map">Specifies mapping to use.</param>
        void OneToOne<TTarget>(Expression<Func<T, TTarget>> navigationProperty, Expression<Func<TTarget, T>> inverseProperty, bool isNullable = false, IDataMap map = null)
             where TTarget : class, IDataObject;

        /// <summary>
        /// Maps a one-to-many relationship.
        /// Also called "Reference" or "HasOne" relationship.
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="isNullable">Indicates wether or not <see cref="navigationProperty"/> is nullable in the database.</param>
        /// <param name="columnName">Specifies the column name to use.</param>
        void OneToMany<TTarget>(Expression<Func<T, TTarget>> navigationProperty, Expression<Func<TTarget, ICollection<T>>> withManyProperty, bool isNullable = false, IDataMap map = null)
             where TTarget : class, IDataObject;

        /// <summary>
        /// Maps a one-to-many relationship.
        /// Also called "Reference" or "HasOne" relationship.
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="isNullable">Indicates wether or not <see cref="navigationProperty"/> is nullable in the database.</param>
        /// <param name="columnName">Specifies the column name to use.</param>
        void OneToMany<TTarget, TKey>(Expression<Func<T, TTarget>> navigationProperty, Expression<Func<TTarget, ICollection<T>>> withManyProperty, bool isNullable = false, Expression<Func<T, TKey>> foreignKeyProperty = null)
             where TTarget : class, IDataObject;

        #endregion
    }
}
