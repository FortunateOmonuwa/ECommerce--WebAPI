using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.CategoryDTO
{
    public class CategoryGetDTO
    {
        [Key]
        public int CategoryId { get; set; }
        [Required, StringLength(20)]
        public string Name { get; set; }
        [Required, StringLength(150)]
        public string Description { get; set; }
    }
}
