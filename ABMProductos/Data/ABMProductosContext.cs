using Microsoft.EntityFrameworkCore;
using ABMProductos.Entities;

namespace ABMProductos.Data;

public class ABMProductosContext : DbContext
{
    public ABMProductosContext(
        DbContextOptions<ABMProductosContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Teclado",
                Price = 15000m
            },
            new Product
            {
                Id = 2,
                Name = "Mouse",
                Price = 8000m
            },
            new Product
            {
                Id = 3,
                Name = "Monitor",
                Price = 120000m
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
