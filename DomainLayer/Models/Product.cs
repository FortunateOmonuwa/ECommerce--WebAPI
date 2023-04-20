using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity
        {
            get { return quantity; } 
            set
            {
                quantity = value;
                if (quantity < 1)
                {
                    IsAvailable = false;
                }
                else
                {
                    IsAvailable = true;
                }
            }
        }
        public bool IsAvailable 
        {
            get { return isAvailable; }
            set { isAvailable = value; }
        }

        [Required, DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        public ICollection<Review> Review { get; set; }
        public ICollection<ProductOrder> Orders { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }



        //fields
        private int quantity;
        private bool isAvailable;

    }
}
