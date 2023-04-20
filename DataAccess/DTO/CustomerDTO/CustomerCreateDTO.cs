using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.CustomerDTO
{
    public class CustomerCreateDTO
    {
        [Required]
        [MaxLength(20)]
        [DisplayName("FirstName")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("LastName")]
        public string LastName { get; set; }
        [EmailAddress]
        [DisplayName("Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string Address { get; set; }
    }
}
