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
}