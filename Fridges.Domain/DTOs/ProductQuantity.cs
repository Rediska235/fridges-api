using Fridges.Domain.Entities;

namespace Fridges.Domain.DTOs;

public class ProductQuantity
{
    public Product  Product { get; set; }

    public int Quanity { get; set; }
}
