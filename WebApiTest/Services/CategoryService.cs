using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.DTOs;
using WebApiTest.Interfaces;
using WebApiTest.Models;

namespace WebApiTest.Services
{
    public class CategoryService : ICategoryService
    {
        private static readonly List<Category> categories = new List<Category>();
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        //Get all categories
        public List<GetCategoriesDTO> GetAllCategories()
        {
            return _mapper.Map<List<GetCategoriesDTO>>(categories);

        }

        //Create categrories
        public GetCategoriesDTO? CreateCategories(CategoryCreateDTO categoryData)
        {
            var newCategory = _mapper.Map<Category>(categoryData);
            newCategory.CategoryId = Guid.NewGuid();
            newCategory.Description = categoryData.Description;

            categories.Add(newCategory);
    
            return _mapper.Map<GetCategoriesDTO>(newCategory);

        }

        // Update categories
        public GetCategoriesDTO? UpdateCategories(Guid categoryId, [FromBody] Category categoryData)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);

            if (foundCategory == null)
            {
                return null;
            }

            _mapper.Map(categoryData, foundCategory);
            //foundCategory.Name = categoryData.Name;
            //foundCategory.Description = categoryData.Description;

            return _mapper.Map<GetCategoriesDTO>(foundCategory);


        }

        // Delete categories
        public bool DeleteCategories(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);

            if (foundCategory == null)
            {
                return false;
            }
            categories.Remove(foundCategory);
            return true;
        }

    }
}
