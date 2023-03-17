namespace Fridges.Application.DTOs;

public class AddProductsDto
{
    public Guid FridgeId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}
