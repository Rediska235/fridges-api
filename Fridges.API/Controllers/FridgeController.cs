using Microsoft.AspNetCore.Mvc;

namespace Fridges.API.Controllers;

[ApiController]
[Route("/api/fridges")]
public class FridgeController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllFridges()
    {
        return Ok("fridges");
    }
}
