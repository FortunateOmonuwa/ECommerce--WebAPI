using AutoMapper;
using DataAccess.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Automapper
{
    public class ReviewMapper : Profile
    {
        public ReviewMapper()
        {
            CreateMap<ReviewCreateDTO, Review>().ReverseMap();
            CreateMap<Review, ReviewGetDTO>().ReverseMap();
        }  
    }
}
