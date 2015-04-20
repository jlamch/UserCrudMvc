using System;
using Database.Domain.Repositories;
using Database.EF;

namespace Database.Repository
{
    /// <summary>
    /// Access to all repositories.
    /// </summary>
    public class RepositoryContext : IRepositoryContext, IInitializeDatabase, IDisposable
    {
        /// <summary>
        /// Database context.
        /// </summary>
        private readonly IMyContext databaseContext;

        /// <summary>
        /// User/license repository.
        /// </summary>
        private IUserRepository users;

        /// <summary>
        /// Entities factory.
        /// </summary>
        private IEntityFactory entityFactory;

        /// <summary>
        /// Initializes a new instance of the RepositoryContext class with context.
        /// </summary>
        /// <param name="emsContext">Database context.</param>
        public RepositoryContext(IMyContext emsContext)
        {
            databaseContext = emsContext;
        }

        /// <summary>
        /// Gets users repository.
        /// </summary>
        /// <value>Data repository.</value>
        public IUserRepository Users
        {
            get
            {
                users = users ?? new UserRepository(databaseContext);
                return users;
            }
        }

        /// <summary>
        /// Creates new objects from database context.
        /// </summary>
        /// <value>Entity factory.</value>
        public IEntityFactory EntityFactory
        {
            get
            {
                entityFactory = entityFactory ?? new EntityFactory();
                return entityFactory;
            }
        }

        /// <summary>
        /// Save all changes on shared database context.
        /// </summary>
        /// <returns>Number of changes made.</returns>
        public int SaveChanges()
        {
            return databaseContext.SaveChanges();
        }

        /// <summary>
        /// Execute the strategy to initialize the database.
        /// </summary>
        public void Initialize()
        {
            System.Data.Entity.Database.SetInitializer(new DbInitializer());
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

            if (users != null)
            {
                users.Dispose();
            }

            if (databaseContext != null)
            {
                databaseContext.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}