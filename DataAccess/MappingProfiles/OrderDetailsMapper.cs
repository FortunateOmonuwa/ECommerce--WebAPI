using AutoMapper;
using DataAccess.DTO.OrderDetailsDTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MappingProfiles
{
    public class OrderDetailsMapper : Profile
    {
        public OrderDetailsMapper()
        {
            CreateMap<OrderDetailsCreateDTO, OrderDetails>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsGetDTO>().ReverseMap();
        }
    }
}
