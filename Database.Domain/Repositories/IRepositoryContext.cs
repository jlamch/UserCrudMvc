namespace Database.Domain.Repositories
{
    /// <summary>
    /// Access to all repositories.
    /// </summary>
    public interface IRepositoryContext
    {
        /// <summary>
        /// Gets users repository.
        /// </summary>
        /// <value>Data repository.</value>
        IUserRepository Users { get; }

        /// <summary>
        /// Creates new objects from database context.
        /// </summary>
        /// <value>Entity factory.</value>
        IEntityFactory EntityFactory { get; }

        /// <summary>
        /// Save all changes on shared database context.
        /// </summary>
        /// <returns>Number of changes made.</returns>
        int SaveChanges();
    }
}