using System.ComponentModel.DataAnnotations;

namespace Fridges.Domain.Entities;

public class FridgeProduct
{
    [Key]
    public Guid Id { get; set; }

    public Fridge Fridge { get; set; }

    public Product Product { get; set; }

    public int Quantity { get; set; }
}
