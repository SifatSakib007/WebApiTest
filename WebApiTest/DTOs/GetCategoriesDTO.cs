using System.ComponentModel.DataAnnotations;

namespace WebApiTest.DTOs
{
    public class GetCategoriesDTO
    {
        public Guid CategoryId { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
