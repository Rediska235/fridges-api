using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet, Authorize]
    public IActionResult GetAllProducts()
    {
        return Ok(_service.GetAllProducts());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetProductById(Guid id)
    {
        return Ok(_service.GetProductById(id));
    }

    [HttpPost, Authorize(Roles = "Product-maker")]
    public IActionResult CreateProduct(CreateProductDto createProductDto)
    {
        var product = _service.CreateProduct(createProductDto);
        return Created($"/api/products/{product.Id}", product);
    }

    [HttpPut, Authorize(Roles = "Product-maker")]
    public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
    {
        var product = _service.UpdateProduct(updateProductDto);
        return Ok(product);
    }

    [HttpDelete("{id:guid}"), Authorize(Roles = "Product-maker")]
    public IActionResult DeleteProduct(Guid id)
    {
        _service.DeleteProduct(id);
        return NoContent();
    }
}
