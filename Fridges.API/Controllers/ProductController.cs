using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Application.Services.Services;
using Fridges.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.API.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        return Ok(_service.GetAllProducts());
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(Guid id)
    {
        return Ok(_service.GetProductById(id));
    }

    [HttpPost]
    public IActionResult CreateProduct(CreateProductDto Product)
    {
        var product = _service.CreateProduct(Product);
        return Created($"/api/products/{product.Id}", product);
    }

    [HttpPut]
    public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
    {
        var product = _service.UpdateProduct(updateProductDto);
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(Guid id)
    {
        _service.DeleteProduct(id);
        return NoContent();
    }
}
