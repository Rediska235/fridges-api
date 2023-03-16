using Fridges.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Fridges.API.DTOs;

public class FridgeCreateDto
{

    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(30)]
    public string? OwnerName { get; set; }

    public FridgeModel FridgeModel { get; set; }
}
