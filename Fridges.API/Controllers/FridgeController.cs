using Fridges.Application.Interfaces.Services;
using Fridges.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.API.Controllers;

[ApiController]
[Route("/api/fridges")]
public class FridgeController : ControllerBase
{
    private readonly IFridgeService _service;

    public FridgeController(IFridgeService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllFridges()
    {
        return Ok(_service.GetAllFridges());
    }

    [HttpGet("{id}")]
    public IActionResult GetFridgeById(Guid id)
    {
        return Ok(_service.GetFridgeById(id));
    }

    [HttpPost]
    public IActionResult CreateFridge(Fridge Fridge)
    {
        _service.CreateFridge(Fridge);
        return Ok();    //Created
    }

    [HttpPatch]
    public IActionResult UpdateFridge(Fridge Fridge)
    {
        _service.UpdateFridge(Fridge);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteFridge(Guid id)
    {
        _service.DeleteFridge(id);
        return Ok();
    }
}
