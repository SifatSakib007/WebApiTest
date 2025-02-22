using Microsoft.EntityFrameworkCore;
using WebApiTest.Models;

namespace WebApiTest.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<Category> categories { get; set; }

    }
}
