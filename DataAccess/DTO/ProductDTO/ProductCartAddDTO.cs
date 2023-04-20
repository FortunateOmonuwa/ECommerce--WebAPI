using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.ProductDTO
{
    public class ProductCartAddDTO
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
