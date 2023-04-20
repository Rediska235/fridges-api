using System.ComponentModel.DataAnnotations;

namespace Fridges.API.DTOs;

public class CreateFridgeDto
{
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(30)]
    public string? OwnerName { get; set; }

    public Guid FridgeModelId { get; set; }
}
