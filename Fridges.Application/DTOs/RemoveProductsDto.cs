namespace Fridges.Application.DTOs;

public class RemoveProductsDto
{
    public Guid FridgeId { get; set; }

    public Guid ProductId { get; set; }
}
