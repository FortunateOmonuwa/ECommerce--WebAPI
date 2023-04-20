using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class ReviewGetDTO
    {
        public int ReviewId { get; set; }
        //public int ReviewerId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ReviewTime { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }

    }
}
