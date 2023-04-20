using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime CreateAt { get; set; } = DateTime.Now;
        [DataType(DataType.DateTime)]
        public DateTime? UpdateAt { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
