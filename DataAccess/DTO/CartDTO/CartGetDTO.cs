using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DTO.ProductDTO;

namespace DataAccess.DTO.CartDTO
{
    public class CartGetDTO
    {
        [Key]
        public int CartId { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime CreateAt { get; set; } = DateTime.Now;
        [Required, DataType(DataType.DateTime)]
        public DateTime? UpdateAt { get; set; }
        public ICollection<ProductGetDTO> Products { get; set; }
    }
}
