using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.API.DTOs;

public class FridgeWithProductsDto
{
    public Fridge Fridge { get; set; }

    public IEnumerable<ProductQuantity> Products { get; set; }
}
