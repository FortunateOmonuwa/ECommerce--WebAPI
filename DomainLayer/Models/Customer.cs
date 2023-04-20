using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
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
        [MaxLength(50)]
        [DisplayName("Home Address")]
        public string Address { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
