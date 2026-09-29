using ABMProductos.Models.DTOs.Requests;
using ABMProductos.Models.DTOs.Responses;

namespace ABMProductos.Services.Interfaces;

public interface IProductService
{
    List<ProductForReadDto> GetAllProducts();
    ProductForReadDto? GetProductById(int id);
    ProductForReadDto? CreateProduct(ProductForCreateDto dto);
    void UpdateProduct(int id, ProductForUpdateDto dto);
    void DeleteProduct(int id);
    
    List<ProductForReadDto> SearchProductsByName(string name);
    ProductStatsDto GetStats();
}