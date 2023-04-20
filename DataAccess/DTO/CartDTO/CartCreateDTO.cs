using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.CartDTO
{
    public class CartCreateDTO
    {
        [Required, DataType(DataType.DateTime)]
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
