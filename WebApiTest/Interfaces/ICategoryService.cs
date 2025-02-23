using Microsoft.AspNetCore.Mvc;
using WebApiTest.Controllers;
using WebApiTest.DTOs;
using WebApiTest.Models;

namespace WebApiTest.Interfaces
{
    public interface ICategoryService
    {
        Task<PaginatedResult<GetCategoriesDTO?>> GetAllCategories(int pageNumber, int pageSize, 
                        string? search = null, string? sortOrder = null);
        Task<GetCategoriesDTO?> CreateCategories(CategoryCreateDTO categoryData);
        Task<GetCategoriesDTO> UpdateCategories(Guid categoryId, [FromBody] Category categoryData);
        Task<bool> DeleteCategories(Guid categoryId);
    }
}
