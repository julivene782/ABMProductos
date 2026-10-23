using ABMProductos.Data;
using ABMProductos.Entities;
using ABMProductos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ABMProductos.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly ABMProductosContext _context;

    public ProductRepository(ABMProductosContext context)
    {
        _context = context;
    }

    public List<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public Product? GetProductById(int id)
    {
        return _context.Products
            .FirstOrDefault(p => p.Id == id);
    }

    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    public List<Product> SearchProductsByName(string name)
    {
        return _context.Products
            .Where(p => EF.Functions.Like(
                p.Name.ToLower(), 
                $"%{name.ToLower()}%"))
            .ToList();
    }
}
