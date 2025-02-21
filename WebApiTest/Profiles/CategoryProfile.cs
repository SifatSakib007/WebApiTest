using AutoMapper;
using WebApiTest.DTOs;
using WebApiTest.Models;

namespace WebApiTest.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoriesDTO>();
            CreateMap<CategoryCreateDTO, Category>();
        }
    }
}
