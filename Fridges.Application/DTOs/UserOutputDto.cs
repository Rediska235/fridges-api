using Fridges.Domain.Entities;

namespace Fridges.Application.DTOs;

public class UserOutputDto
{
    public string Username { get; set; }

    public List<Role> Roles { get; set; } = new List<Role>();
}
