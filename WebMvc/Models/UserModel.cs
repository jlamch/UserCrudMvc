using System.ComponentModel.DataAnnotations;
using Database.Domain.Entities;

namespace WebMvc.Models
{
    public class UserModel : IUser
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [StringLength(1000)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+?\d+(-\d+)*$", ErrorMessage = "Entered phone format is not valid.")]
        [Display(Name = "Telephone number")]
        public string TelephoneNumber { get; set; }

        public long Id { get; set; }
    }
}