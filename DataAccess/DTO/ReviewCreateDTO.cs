using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class ReviewCreateDTO
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
