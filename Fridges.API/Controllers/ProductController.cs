using Fridges.Application.Interfaces.Services;
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
    public IActionResult CreateProduct(Product product)
    {
        _service.CreateProduct(product);
        return Ok();    //Created
    }

    [HttpPatch]
    public IActionResult UpdateProduct(Product product)
    {
        _service.UpdateProduct(product);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(Guid id)
    {
        _service.DeleteProduct(id);
        return Ok();
    }
}
