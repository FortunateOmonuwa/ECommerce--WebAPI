using AutoMapper;
using DataAccess.DTO.CustomerDTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MappingProfiles
{
    public class CustomerMapper : Profile
    {
        public CustomerMapper() 
        {
            CreateMap<CustomerCreateDTO, Customer>().ReverseMap();
            CreateMap<Customer, CustomerGetDTO>().ReverseMap();
        }
    }
}
