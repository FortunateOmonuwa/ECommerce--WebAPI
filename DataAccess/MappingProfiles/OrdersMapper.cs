using AutoMapper;
using DataAccess.DTO.OrderDetailsDTO;
using DataAccess.DTO.OrdersDTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Automapper
{
    public class OrdersMapper : Profile
    {
        public OrdersMapper()
        {
            CreateMap<OrdersCreateDTO, Order>().ReverseMap();
            CreateMap<Order, OrdersGetDTO>().ReverseMap();
        }
    }
}
