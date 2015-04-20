using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Domain.Entities;

namespace Database.EF.Entities
{
    /// <summary>
    /// User entitie.
    /// </summary>
    public class User : IUser
    {
        /// <summary>
        /// Gets or sets user identifier.
        /// </summary>
        /// <value>Id of user.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; protected set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required, StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>The surname.</value>
        [Required, StringLength(200)]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        [StringLength(1000)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the telephone number.
        /// </summary>
        /// <value>The telephone number.</value>
        [StringLength(20)]
        public string TelephoneNumber { get; set; }
    }
}