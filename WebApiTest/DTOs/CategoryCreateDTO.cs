using System.ComponentModel.DataAnnotations;

namespace WebApiTest.DTOs
{
    public class CategoryCreateDTO
    {
        [Required(ErrorMessage ="Category name is required")]
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
