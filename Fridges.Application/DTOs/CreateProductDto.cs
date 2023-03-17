using System.ComponentModel.DataAnnotations;

namespace Fridges.API.DTOs;

public class CreateProductDto
{
    [MaxLength(30)]
    public string Name { get; set; }

    public int? DefaultQuantity { get; set; }
}
