using Fridges.API.DTOs;
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
    public IActionResult CreateFridge(FridgeCreateDto Fridge)
    {
        var fridge = _service.CreateFridge(Fridge);
        return Created($"/api/fridges/{fridge.Id}", fridge);
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

    /*[HttpPatch("{id}")]
    public IActionResult AddProducts(Guid id)
    {
        _service.AddProducts(id);
        return Ok();
    }

    [HttpPatch("{id}")]
    public IActionResult RemoveProducts(Guid id)
    {
        _service.RemoveProducts(id);
        return Ok();
    }*/
}
