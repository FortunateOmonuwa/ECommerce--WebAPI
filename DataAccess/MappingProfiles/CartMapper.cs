using AutoMapper;
using DataAccess.DTO.CartDTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MappingProfiles
{
    public class CartMapper : Profile
    {
        public CartMapper()
        {
            CreateMap<CartCreateDTO, Cart>().ReverseMap();
            CreateMap<Cart, CartGetDTO>().ReverseMap();
        }
    }
}
