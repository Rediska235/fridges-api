using Fridges.API.DTOs;
using Fridges.Application.DTOs;
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
    public IActionResult GetProductsByFridgeId(Guid id)
    {
        var fridge = _service.GetFridgeById(id);
        var products = _service.GetProductsByFridgeId(id);
        var result = new FridgeWithProductsDto
        {
            Fridge = fridge,
            Products = products
        };

        return Ok(result);
    }

    [HttpPost("{fridgeId}/products")]
    public IActionResult AddProducts(Guid fridgeId, Guid productId, [FromBody] int quantity)
    {
        AddProductsDto addProductsDto = new()
        {
            FridgeId = fridgeId,
            ProductId = productId,
            Quantity = quantity
        };
        _service.AddProducts(addProductsDto);
        return Ok();
    }

    [HttpDelete("{fridgeId}/products/{productId}")]
    public IActionResult RemoveProducts(Guid fridgeId, Guid productId)
    {
        _service.RemoveProducts(fridgeId, productId);
        return Ok();
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
}
