using Database.Domain.Entities;

namespace Database.Domain.Repositories
{
    /// <summary>
    /// Creates new objects from database context.
    /// </summary>
    public interface IEntityFactory
    {
        /// Creates new user object.
        /// </summary>
        /// <returns>User object.</returns>
        IUser User();
    }
}