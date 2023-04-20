using System.ComponentModel.DataAnnotations;

namespace Fridges.Application.DTOs;

public class UpdateFridgeModelDto
{
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }

    public int? Year { get; set; }
}
