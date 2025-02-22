using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;
using WebApiTest.DTOs;
using WebApiTest.Interfaces;
using WebApiTest.Models;
using WebApiTest.Services;

namespace WebApiTest.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categoryList = await _categoryService.GetAllCategories();
            if (categoryList.Count != 0)
                return Ok(ApiResponse<List<GetCategoriesDTO>>.SuccessResponse(categoryList!.Cast<GetCategoriesDTO>().ToList(), 200, "categories returned successfully."));
            else
                return Ok(ApiResponse<List<GetCategoriesDTO>>.SuccessResponse(categoryList!.Cast<GetCategoriesDTO>().ToList(), 200, "categories empty."));
        }

        //Post: /api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO categoryData)
        {
            var newCategories = await _categoryService.CreateCategories(categoryData);
            if (newCategories == null)
            {
                return BadRequest("Failed to create category.");
            }
            return Created($"/api/categories{newCategories.CategoryId}", newCategories);
        }

        //Put: /api/categories/{categoryId} => Update a category by id
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody] Category categoryData)
        {
            var foundCategory = await _categoryService.UpdateCategories(categoryId, categoryData);
            if (foundCategory == null)
            {
                return NoContent();
            }
            return Ok("Something wrong");

        }

        //Delete: /api/categories/{categoryId} => Delete a category by id
        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            var foundCategory = await _categoryService.DeleteCategories(categoryId);
            if (foundCategory)
            {
                return NoContent();
            }
            return Ok("Something wrong");
        }

        /*        // Get: /api/categories
                [HttpGet]
                public IActionResult GetCategories([FromQuery] string searchValue = "")
                {
                    List<Category> searchCategories = new List<Category>();
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        searchCategories = categories.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                        return Ok(searchCategories);
                    }
                    if (searchCategories.Count == 0)
                    {
                        return Ok(ApiResponse<List<Category>>.SuccessResponse(searchCategories, 200, "categories empty"));
                    }
                    return Ok(ApiResponse<List<Category>>.SuccessResponse(searchCategories, 200, "categories returned successfully"));
                }*/

        /*      

               

                
        */
    }
}
