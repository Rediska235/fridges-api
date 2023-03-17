using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Application.Interfaces.Services;
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
    public IActionResult GetProductsByFridgeId(Guid id)
    {
        var fridge = _service.GetFridgeById(id);
        var products = _service.GetProductsByFridgeId(id);
        var fridgeWithProducts = new FridgeWithProductsDto
        {
            Fridge = fridge,
            Products = products
        };

        return Ok(fridgeWithProducts);
    }

    [HttpPost("{fridgeId}/products")]
    public IActionResult AddProducts(Guid fridgeId, AddProductsDto addProductsDto)
    {
        _service.AddProducts(fridgeId, addProductsDto);
        return Ok();
    }

    [HttpDelete("{fridgeId}/products/{productId}")]
    public IActionResult RemoveProducts(Guid fridgeId, Guid productId)
    {
        _service.RemoveProducts(fridgeId, productId);
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateFridge(CreateFridgeDto Fridge)
    {
        var fridge = _service.CreateFridge(Fridge);
        return Created($"/api/fridges/{fridge.Id}", fridge);
    }

    [HttpPut]
    public IActionResult UpdateFridge(UpdateFridgeDto updateFridgeDto)
    {
        _service.UpdateFridge(updateFridgeDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteFridge(Guid id)
    {
        _service.DeleteFridge(id);
        return Ok();
    }
}
