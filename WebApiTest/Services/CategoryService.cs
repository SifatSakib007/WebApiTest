using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Controllers;
using WebApiTest.Data;
using WebApiTest.DTOs;
using WebApiTest.Interfaces;
using WebApiTest.Models;

namespace WebApiTest.Services
{
    public class CategoryService : ICategoryService
    {
        //private static readonly List<Category> categories = new List<Category>();
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        //Get all categories
        public async Task<PaginatedResult<GetCategoriesDTO?>> GetAllCategories(int pageNumber, int pageSize, string? search = null)
        {
            IQueryable<Category> query = _context.categories;

            //search by name or description
            if (!string.IsNullOrWhiteSpace(search?.ToLower()))
            {
                var formattedSearch = $"%{search.Trim()}%";
                //query = query.Where(c => c.Name.ToLower().Contains(search) || c.Description.ToLower().Contains(search));
                query = query.Where(c => EF.Functions.Like(c.Name, formattedSearch) || EF.Functions.Like(c.Description, formattedSearch));
            }

            //get total count
            var totalCount = await query.CountAsync();

            //pagination, pageNumber = 3, pagesize = 5
            //20 categories
            //Skip((pageNumber-1)*pageSize).Take(pageSize)
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var results = _mapper.Map<List<GetCategoriesDTO?>>(items);

            return new PaginatedResult<GetCategoriesDTO?>
            {
                Items = results,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        //Create categrories
        public async Task<GetCategoriesDTO?> CreateCategories(CategoryCreateDTO categoryData)
        {
            var newCategory = _mapper.Map<Category>(categoryData);
            newCategory.CategoryId = Guid.NewGuid();
            newCategory.Description = categoryData.Description;

            await _context.categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
    
            return _mapper.Map<GetCategoriesDTO>(newCategory);

        }

        // Update categories
        public async Task<GetCategoriesDTO?> UpdateCategories(Guid categoryId, [FromBody] Category categoryData)
        {
            var foundCategory = await _context.categories.FindAsync(categoryId);
            if (foundCategory == null)
            {
                return null;
            }

            _mapper.Map(categoryData, foundCategory);
            _context.categories.Update(foundCategory);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetCategoriesDTO>(foundCategory);
        }

        // Delete categories
        public async Task<bool> DeleteCategories(Guid categoryId)
        {
            var foundCategory = await _context.categories.FindAsync(categoryId);

            if (foundCategory == null)
            {
                return false;
            }
            _context.categories.Remove(foundCategory);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
