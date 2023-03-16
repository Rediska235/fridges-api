using System.ComponentModel.DataAnnotations;

namespace Fridges.Domain.Entities;

public class Fridge 
{
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(30)]
    public string? OwnerName { get; set; }

    public FridgeModel FridgeModel { get; set; }

    public List<FridgeProduct> FridgeProducts { get; set; }
}
