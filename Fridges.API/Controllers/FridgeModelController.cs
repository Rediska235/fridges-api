using Fridges.Application.DTOs;
using Fridges.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.API.Controllers;

[ApiController]
[Route("/api/fridgemodels")]
public class FridgeModelController : ControllerBase
{
    private readonly IFridgeModelService _service;

    public FridgeModelController(IFridgeModelService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllFridgeModels()
    {
        return Ok(_service.GetAllFridgeModels());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetFridgeModelById(Guid id)
    {
        return Ok(_service.GetFridgeModelById(id));
    }

    [HttpPost, Authorize(Roles = "Fridge-maker")]
    public IActionResult CreateFridgeModel(CreateFridgeModelDto createFridgeModelDto)
    {
        var fridgeModel = _service.CreateFridgeModel(createFridgeModelDto);
        return Created($"/api/fridgemodels/{fridgeModel.Id}", fridgeModel);
    }

    [HttpPut, Authorize(Roles = "Fridge-maker")]
    public IActionResult UpdateFridgeModel(UpdateFridgeModelDto updateFridgeModelDto)
    {
        var fridgeModel = _service.UpdateFridgeModel(updateFridgeModelDto);
        return Ok(fridgeModel);
    }

    [HttpDelete("{id:guid}"), Authorize(Roles = "Fridge-maker")]
    public IActionResult DeleteFridgeModel(Guid id)
    {
        _service.DeleteFridgeModel(id);
        return NoContent();
    }
}
