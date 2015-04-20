using System;
using System.Linq;
using System.Linq.Expressions;
using Database.Domain.Entities;

namespace Database.Domain.Repositories
{
    /// <summary>
    /// Base repository pattern.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public interface IBaseRepository<T> : IDisposable
        where T : IEntity
    {
        /// <summary>
        /// Total count of elements.
        /// </summary>
        /// <param name="predicate">Specified condition to count a object.</param>
        /// <returns>Elements counter.</returns>
        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets all objects from database.
        /// </summary>
        /// <param name="includes">Specified the related object to include in the query results.</param>
        /// <returns>Object collection.</returns>
        IQueryable<T> All(string includes = null);

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate">Specified condition to find a object.</param>
        /// <param name="includes">Specified the related object to include in the query results.</param>
        /// <returns>Founded object.</returns>
        T Find(Expression<Func<T, bool>> predicate, string includes = null);

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size.</param>
        /// <returns>Filtered objects.</returns>
        IQueryable<T> Filter(out int total, int index = 0, int size = 20);

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="t">Specified a new object to create.</param>
        /// <returns>Created object.</returns>
        T Create(T t);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="t">Specified a existing object to delete.</param>
        /// <returns>Created object.</returns>
        int Delete(T t);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        /// <returns>The number of objects updated in database.</returns>
        int Update(T t);
    }
}