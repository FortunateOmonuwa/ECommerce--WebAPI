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
    public class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        [Phone]
        public string CustomerPhoneNumber { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
