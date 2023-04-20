using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class ProductOrder
    {
        public int ProductOrderId { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
    }
}