using Database.Domain.Entities;
using Database.Domain.Repositories;
using Database.EF.Entities;

namespace Database.Repository
{
    /// <summary>
    /// Creates new objects from database context.
    /// </summary>
    public class EntityFactory : IEntityFactory
    {
        /// <summary>
        /// Creates new user object.
        /// </summary>
        /// <returns>User object.</returns>
        public IUser User()
        {
            return new User();
        }
    }
}