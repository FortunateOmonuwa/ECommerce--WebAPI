using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public ICollection<ProductOrder> Products { get; set; }
    }
}

