using Microsoft.AspNetCore.Mvc;
using WebApiTest.DTOs;
using WebApiTest.Models;

namespace WebApiTest.Interfaces
{
    public interface ICategoryService
    {
        List<GetCategoriesDTO> GetAllCategories();
        GetCategoriesDTO? CreateCategories(CategoryCreateDTO categoryData);
        GetCategoriesDTO UpdateCategories(Guid categoryId, [FromBody] Category categoryData);
        bool DeleteCategories(Guid categoryId);
    }
}
