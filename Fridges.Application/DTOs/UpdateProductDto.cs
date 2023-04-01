using System.ComponentModel.DataAnnotations;

namespace Fridges.Application.DTOs;

public class UpdateProductDto
{
    public Guid Id { get; set; }

    [MaxLength(30)]
    public string Name { get; set; }

    public int? DefaultQuantity { get; set; }
    public Guid? ImageId { get; set; }
}
