using System.ComponentModel.DataAnnotations;

namespace Fridges.Application.DTOs;

public class CreateFridgeModelDto
{
    [MaxLength(50)]
    public string Name { get; set; }

    public int? Year { get; set; }
}
