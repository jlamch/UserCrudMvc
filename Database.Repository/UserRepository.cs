using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Database.Domain.Entities;
using Database.Domain.Repositories;
using Database.EF;
using Database.EF.Entities;

namespace Database.Repository
{
    /// <summary>
    /// Repository of users.
    /// </summary>
    public class UserRepository : BaseRepository<User, IUser>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the UserRepository class with context.
        /// </summary>
        /// <param name="objectContext">Database context.</param>
        public UserRepository(IMyContext objectContext)
            : base(objectContext)
        {
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public void DeleteUser(long userId)
        {
            var toDelete = MyContext.Users.FirstOrDefault(x => x.Id == userId);
            MyContext.Users.Remove(toDelete);
            MyContext.SaveChanges();
        }

        /// <summary>
        /// Gets user by idenfirier.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>User object.</returns>
        public IUser Get(long userId)
        {
            return MyContext.Users.FirstOrDefault(x => x.Id == userId);
        }

        /// <summary>
        /// Gets the user list .
        /// </summary>
        /// <returns>User list.</returns>
        public IEnumerable<IUser> GetUserList()
        {
            return base.All().ToList();
        }

        /// <summary>
        /// Gets the by surname.
        /// </summary>
        /// <param name="surname"></param>
        /// <returns></returns>
        public IEnumerable<IUser> GetBySurname(string surname)
        {
            return MyContext.Users.Where(x => x.Surname == surname).ToList();
        }

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="surname">The surname.</param>
        /// <param name="address">The address.</param>
        /// <param name="telephoneNumber">The telephone number.</param>
        /// <returns></returns>
        public IUser AddUser(string name, string surname, string address, string telephoneNumber)
        {
            var user = new User()
            {
                Name = name,
                Surname = surname,
                Address = address,
                TelephoneNumber = telephoneNumber
            };

            var created = MyContext.Users.Add(user);
            MyContext.SaveChanges();
            return created;
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="surname">The surname.</param>
        /// <param name="address">The address.</param>
        /// <param name="telephoneNumber">The telephone number.</param>
        /// <returns></returns>
        public bool UpdateUser(long id, string name, string surname, string address, string telephoneNumber)
        {
            var instanceToUpdate = MyContext.Users.FirstOrDefault(i => i.Id == id);

            instanceToUpdate.Name = name;
            instanceToUpdate.Surname = surname;
            instanceToUpdate.Address = address;
            instanceToUpdate.TelephoneNumber = telephoneNumber;

            return MyContext.SaveChanges() > 0;
        }

        /// <summary>
        /// Provide default result sorting.
        /// </summary>
        /// <param name="source">Set of querable data.</param>
        /// <returns>Sorted querable dataset.</returns>
        protected override IOrderedQueryable<User> DefaultSort(DbSet<User> source)
        {
            return source.OrderBy(x => x.Surname);
        }
    }
}