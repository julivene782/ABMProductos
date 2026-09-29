using ABMProductos.Models.DTOs.Requests;
using ABMProductos.Models.DTOs.Responses;
using ABMProductos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ABMProductos.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<ProductForReadDto>> GetAllProducts()
    {
        var products = _service.GetAllProducts();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public ActionResult<ProductForReadDto> GetProductById(int id)
    {
        var product = _service.GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<ProductForReadDto> CreateProduct(ProductForCreateDto dto)
    {
        var product = _service.CreateProduct(dto);

        if (product == null)
        {
            return Conflict("Ya existe un producto con ese nombre.");
        }

        return CreatedAtAction(
            nameof(GetProductById),
            new { id = product.Id },
            product
        );
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, ProductForUpdateDto dto)
    {
        var product = _service.GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }

        _service.UpdateProduct(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _service.GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }

        _service.DeleteProduct(id);

        return NoContent();
    }

    [HttpGet("search")]
    public ActionResult<List<ProductForReadDto>> SearchProductsByName([FromQuery] string name)
    {
        var products = _service.SearchProductsByName(name);

        return Ok(products);
    }

    [HttpGet("stats")]
    public ActionResult<ProductStatsDto> GetStats()
    {
        var stats = _service.GetStats();

        return Ok(stats);
    }
}