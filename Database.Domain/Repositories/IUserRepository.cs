using System.Collections.Generic;
using Database.Domain.Entities;

namespace Database.Domain.Repositories
{
    /// <summary>
    /// Repository of users.
    /// </summary>
    public interface IUserRepository : IBaseRepository<IUser>
    {
        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        void DeleteUser(long userId);

        /// <summary>
        /// Gets the specified user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        IUser Get(long userId);

        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IUser> GetUserList();

        /// <summary>
        /// Gets the by surname.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IUser> GetBySurname(string surname);

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="surname">The surname.</param>
        /// <param name="address">The address.</param>
        /// <param name="telephoneNumber">The telephone number.</param>
        /// <returns></returns>
        IUser AddUser(
            string name,
            string surname,
            string address,
            string telephoneNumber);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="surname">The surname.</param>
        /// <param name="address">The address.</param>
        /// <param name="telephoneNumber">The telephone number.</param>
        /// <returns></returns>
        bool UpdateUser(
            long id,
            string name,
            string surname,
            string address,
            string telephoneNumber);
    }
}