using Fridges.Application.DTOs;
using Fridges.Domain.Entities;

namespace Fridges.Application.Services.Interfaces;

public interface IAuthService
{
    User Register(UserDto request);
    string Login(UserDto request, string secretKey);
    string RefreshToken(string secretKey);
    void GiveRole(GiveRoleDto giveRoleDto);
}
