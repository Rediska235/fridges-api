using Microsoft.AspNetCore.Mvc;

namespace Fridges.API.Controllers;

[ApiController]
[Route("/api/products")]
public class ProductController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllProducts()
    {
        return Ok("products");
    }
}
