using System;
using System.Data.Entity;
using Database.EF.Entities;

namespace Database.EF
{
    /// <summary>
    /// Ems context.
    /// </summary>
    public interface IMyContext
    {
        /// <summary>
        /// Gets or sets collection of users.
        /// </summary>
        /// <value>Collection of users.</value>
        IDbSet<User> Users { get; set; }

        /// <summary>
        /// Gets a Database instance for this context that allows for creation/deletion/existence checks for the underlying database.
        /// </summary>
        /// <value>Database instance.</value>
        System.Data.Entity.Database Database { get; }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        int SaveChanges();

        /// <summary>
        /// Returns a non-generic DbSet instance for access to entities of the given
        ///  type in the context, the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <param name="entityType">The type of entity for which a set should be returned.</param>
        /// <returns>A set for the given entity type.</returns>
        DbSet Set(Type entityType);

        /// <summary>
        /// Returns a DbSet instance for access to entities of the given type in the
        ///  context, the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type entity for which a set should be returned.</typeparam>
        /// <returns>A set for the given entity type.</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Calls the protected Dispose method.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Gets a System.Data.Entity.Infrastructure.DbEntityEntry object for the given
        ///  entity providing access to information about the entity and the ability to
        ///  perform actions on the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns> An entry for the entity.</returns>
        System.Data.Entity.Infrastructure.DbEntityEntry Entry(object entity);

        /// <summary>
        /// Test.
        /// </summary>
        /// <typeparam name="TEntity">Test.</typeparam>
        /// <param name="entity">Test.</param>
        /// <returns>Test.</returns>
        System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}