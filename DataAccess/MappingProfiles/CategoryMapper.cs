using AutoMapper;
using DataAccess.DTO.CategoryDTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Automapper
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper() 
        {
            CreateMap<CategoryCreateDTO, Category>().
                ForMember(dest => dest.CategoryId, opt => opt.Ignore())
    .           ForMember(dest => dest.ProductCategories, opt => opt.Ignore());
            CreateMap<Category, CategoryGetDTO>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
