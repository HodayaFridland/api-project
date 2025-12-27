using Microsoft.EntityFrameworkCore;

namespace api_project.Data
{
    public class ApplicationDbContext: DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Car> Cars => Set<Car>();
    }
}
