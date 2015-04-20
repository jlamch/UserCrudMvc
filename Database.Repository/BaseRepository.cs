using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Database.Domain.Entities;
using Database.Domain.Repositories;
using Database.EF;

namespace Database.Repository
{
    /// <summary>
    /// Base repository class with common for all repositories methods.
    /// </summary>
    /// <typeparam name="TObject">Entity object type.</typeparam>
    /// <typeparam name="TInterface">Entity interface.</typeparam>
    public abstract class BaseRepository<TObject, TInterface> : IBaseRepository<TInterface>
        where TObject : class, TInterface
        where TInterface : IEntity
    {
        /// <summary>
        /// True if the contaxt is shared between repository opetations, False save changes for each operation.
        /// </summary>
        private readonly bool shareContext;

        /// <summary>
        /// Initializes a new instance of the BaseRepository class for specific database context.
        /// </summary>
        /// <param name="context">Database context.</param>
        protected BaseRepository(IMyContext context)
        {
            MyContext = context;
            shareContext = true;
        }

        /// <summary>
        /// Gets current database context.
        /// </summary>
        /// <value>Database context.</value>
        protected IMyContext MyContext { get; private set; }

        /// <summary>
        /// Gets all objects list.
        /// </summary>
        /// <value>Colection of entites.</value>
        protected DbSet<TObject> DbSet
        {
            get
            {
                return MyContext.Set<TObject>();
            }
        }

        /// <summary>
        /// Count of elements.
        /// </summary>
        /// <param name="predicate">Specified condition to count a object.</param>
        /// <returns>Elements counter.</returns>
        public int Count(Expression<Func<TInterface, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return DbSet.Where<TInterface>(predicate).Count();
        }

        /// <summary>
        /// Gets all objects from database.
        /// </summary>
        /// <typeparam name="TInterface">Generic type.</typeparam>
        /// <param name="includes">Specified the related objects to include in the query results.</param>
        /// <returns>
        /// Object collection.
        /// </returns>
        public IQueryable<TInterface> All(string includes = null)
        {
            if (includes != null && includes.Any())
            {
                return DbSet.Include(includes).AsQueryable();
            }

            return DbSet.AsQueryable();
        }

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <typeparam name="TInterface">Generic type.</typeparam>
        /// <param name="predicate">Specified condition to find a object.</param>
        /// <param name="includes">Specified the related object to include in the query results.</param>
        /// <returns>
        /// Founded objects.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public TInterface Find(Expression<Func<TInterface, bool>> predicate, string includes = null)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            if (includes != null && includes.Any())
            {
                return DbSet.Include(includes).FirstOrDefault(predicate);
            }

            return DbSet.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size.</param>
        /// <returns>Filtered objects.</returns>
        public IQueryable<TInterface> Filter(out int total, int index = 0, int size = 20)
        {
            return Filter(null, out total, index, size, null);
        }

        /// <summary>
        /// Add object to database.
        /// </summary>
        /// <param name="obj">Specified the object to add.</param>
        /// <returns>Entiti object added to the database context.</returns>
        public virtual TInterface Create(TInterface obj)
        {
            var newEntry = DbSet.Add((TObject)obj);

            if (!shareContext)
            {
                MyContext.SaveChanges();
            }

            return newEntry;
        }

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="obj">Specified a existing object to delete.</param>
        /// <returns>Created object.</returns>
        public virtual int Delete(TInterface obj)
        {
            DbSet.Remove((TObject)obj);
            if (!shareContext)
            {
                return MyContext.SaveChanges();
            }

            return 0;
        }

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="obj">Specified the object to save.</param>
        /// <returns>The number of objects updated in database.</returns>
        public virtual int Update(TInterface obj)
        {
            var entry = MyContext.Entry((TObject)obj);
            DbSet.Attach((TObject)obj);
            entry.State = System.Data.Entity.EntityState.Modified;

            if (!shareContext)
            {
                return MyContext.SaveChanges();
            }

            return 0;
        }

        /// <summary>
        /// Saves all changes made in this context to database.
        /// </summary>
        /// <returns>The number of objects written to database.</returns>
        public virtual int SaveChanges()
        {
            return MyContext.SaveChanges();
        }

        /// <summary>
        /// Dispose object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// The bulk of the clean-up code is implemented in Dispose(bool).
        /// </summary>
        /// <param name="disposing">Disposing parameter.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (MyContext != null)
            {
                MyContext.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate">Specified the filter expression.</param>
        /// <returns>The number of objects written to database.</returns>
        protected virtual int Delete(Expression<Func<TInterface, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
            {
                DbSet.Remove((TObject)obj);
            }

            if (!shareContext)
            {
                return MyContext.SaveChanges();
            }

            return 0;
        }

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter.</param>
        /// <param name="includes">Specified the related object to include in the query results.</param>
        /// <returns>Filtered objects.</returns>
        protected virtual IQueryable<TInterface> Filter(Expression<Func<TInterface, bool>> predicate, string includes = null)
        {
            if (includes != null && includes.Any())
            {
                return DbSet.Include(includes).Where<TInterface>(predicate).AsQueryable<TInterface>();
            }

            return DbSet.Where<TInterface>(predicate).AsQueryable<TInterface>();
        }

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <param name="predicate">Specified a filter.</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size.</param>
        /// <param name="includes">Specified the related objects to include in the query results.</param>
        /// <returns>Filtered objects.</returns>
        protected virtual IQueryable<TInterface> Filter(
            Expression<Func<TInterface, bool>> predicate,
            out int total,
            int index = 0,
            int size = 20,
            string includes = null)
        {
            int skipCount = index * size;
            IQueryable<TInterface> dataSet = DefaultSort(DbSet);

            if (includes != null && includes.Any())
            {
                var query = DbSet.Include(includes);

                dataSet = predicate != null
                          ? dataSet.Where<TInterface>(predicate).AsQueryable()
                          : dataSet.AsQueryable();
            }
            else
            {
                dataSet = predicate != null
                          ? dataSet.Where<TInterface>(predicate).AsQueryable()
                          : dataSet.AsQueryable();
            }

            total = dataSet.Count();
            dataSet = skipCount == 0
                      ? dataSet.Take(size)
                      : dataSet.Skip(skipCount).Take(size);
            return dataSet.AsQueryable();
        }

        /// <summary>
        /// Provide default result sorting.
        /// </summary>
        /// <param name="source">Default sorting for database set.</param>
        /// <returns>Sorted elements list.</returns>
        protected abstract IOrderedQueryable<TObject> DefaultSort(DbSet<TObject> source);
    }
}