using System.ComponentModel.DataAnnotations;

namespace Fridges.Domain.Entities;

public class FridgeModel
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }

    public int? Year { get; set; }
}
