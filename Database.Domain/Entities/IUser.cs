namespace Database.Domain.Entities
{
    /// <summary>
    /// User entity.
    /// </summary>
    public interface IUser : IEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>The surname.</value>
        string Surname { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        string Address { get; set; }

        /// <summary>
        /// Gets or sets the telephone number.
        /// </summary>
        /// <value>The telephone number.</value>
        string TelephoneNumber { get; set; }

        /// <summary>
        /// Gets or sets user identifier.
        /// </summary>
        /// <value>Id of user.</value>
        long Id { get; }
    }
}