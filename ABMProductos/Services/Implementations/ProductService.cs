using ABMProductos.Entities;
using ABMProductos.Models.DTOs.Requests;
using ABMProductos.Models.DTOs.Responses;
using ABMProductos.Repositories.Interfaces;
using ABMProductos.Services.Interfaces;

namespace ABMProductos.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public List<ProductForReadDto> GetAllProducts()
    {
        List<Product> products = _repository.GetAllProducts();

        return products.Select(product => new ProductForReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        }).ToList();
    }

    public ProductForReadDto? GetProductById(int id)
    {
        Product? product = _repository.GetProductById(id);

        if (product == null)
        {
            return null;
        }

        return new ProductForReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }

    public ProductForReadDto? CreateProduct(ProductForCreateDto dto)
    {
        List<Product> products = _repository.GetAllProducts();

        bool nameExists = products.Any(p =>
            p.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase));

        if (nameExists)
        {
            return null;
        }

        Product product = new Product
        {
            Name = dto.Name,
            Price = dto.Price
        };

        _repository.AddProduct(product);

        return new ProductForReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }

    public void UpdateProduct(int id, ProductForUpdateDto dto)
    {
        Product? product = _repository.GetProductById(id);

        if (product == null)
        {
            return;
        }

        product.Name = dto.Name;
        product.Price = dto.Price;

        _repository.UpdateProduct(product);
    }

    public void DeleteProduct(int id)
    {
        Product? product = _repository.GetProductById(id);

        if (product == null)
        {
            return;
        }

        _repository.DeleteProduct(product);
    }

    public List<ProductForReadDto> SearchProductsByName(string name)
    {
        List<Product> products = _repository.SearchProductsByName(name);

        return products.Select(product => new ProductForReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        }).ToList();
    }

    public ProductStatsDto GetStats()
    {
        List<Product> products = _repository.GetAllProducts();

        if (products.Count == 0)
        {
            return new ProductStatsDto
            {
                Total = 0,
                AveragePrice = 0,
                MostExpensiveName = string.Empty
            };
        }

        return new ProductStatsDto
        {
            Total = products.Count(),
            AveragePrice = products.Average(p => p.Price),
            MostExpensiveName = products
                .OrderByDescending(p => p.Price)
                .First()
                .Name
        };
    }
}