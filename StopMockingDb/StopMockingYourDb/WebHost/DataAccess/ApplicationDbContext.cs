using Microsoft.EntityFrameworkCore;
using WebHost.Models;

namespace WebHost.DataAccess
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var productReviewEntity = modelBuilder.Entity<ProductReview>();
            productReviewEntity.HasIndex(x => x.UserId).IsUnique();
        }
    }
}
