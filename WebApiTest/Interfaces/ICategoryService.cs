using Microsoft.AspNetCore.Mvc;
using WebApiTest.DTOs;
using WebApiTest.Models;

namespace WebApiTest.Interfaces
{
    public interface ICategoryService
    {
        Task<List<GetCategoriesDTO?>> GetAllCategories();
        Task<GetCategoriesDTO?> CreateCategories(CategoryCreateDTO categoryData);
        Task<GetCategoriesDTO> UpdateCategories(Guid categoryId, [FromBody] Category categoryData);
        Task<bool> DeleteCategories(Guid categoryId);
    }
}
