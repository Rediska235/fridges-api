using System.ComponentModel.DataAnnotations;

namespace Fridges.Domain.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(30)]
    public string Name { get; set; }

    public int? DefaultQuantity { get; set; }

    public List<FridgeProduct> FridgeProducts { get; set; }
}
