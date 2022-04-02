using AutoMapper;
using DecaBlog.Models;
using DecaBlog.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Commons.MappingProfiles
{
   public  class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryToReturnDto>().ForMember("Id", src => src.MapFrom(src => src.Id));
            CreateMap<Category, CategoryToAddDto>().ReverseMap();
        }
    }   
}
