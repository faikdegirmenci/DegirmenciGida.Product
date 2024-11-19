using DegirmenciGida.Product.Domain;
using Microsoft.EntityFrameworkCore;

namespace DegirmenciGida.Product.Infrastructure
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }


        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
                .Property(c => c.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Products>()
                .Property(p=>p.Price).HasPrecision(18,4);
        }
    }
}
