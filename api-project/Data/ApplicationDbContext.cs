using api_project.Models;
using Microsoft.EntityFrameworkCore;

namespace api_project.Data
{
    public class ApplicationDbContext: DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Categories> Categories => Set<Categories>();
        public DbSet<Orders> Orders => Set<Orders>();
        public DbSet<Gifts> Gifts => Set<Gifts>();
        public DbSet<Donors> Donors => Set<Donors>();
        public DbSet<Users> Users => Set<Users>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Donors>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DonorName).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasMany(e => e.Gifts)
                      .WithOne(g => g.donors)
                      .HasForeignKey(g => g.DonorId);
            });
        }

    }
}
