using Fridges.Application.DTOs;
using Fridges.Application.Repositories;
using Fridges.Application.Services.Interfaces;
using Fridges.Domain.Entities;
using Fridges.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Fridges.Application.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly IRoleRepository _roleRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IUserRepository repository, IRoleRepository roleRepository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _roleRepository = roleRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public User Register(UserDto request)
    {
        var user = _repository.GetUserByUsername(request.Username);
        if (user != null)
        {
            throw Exceptions.usernameIsTaken;
        }

        user = new User()
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _repository.InsertUser(user);
        _repository.Save();

        return user;
    }

    public string Login(UserDto request, string secretKey)
    {
        var user = _repository.GetUserByUsername(request.Username);
        if (user == null)
        {
            throw Exceptions.invalidCredential;
        }

        var isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (isValidPassword == false)
        {
            throw Exceptions.invalidCredential;
        }

        string token = JwtManager.CreateToken(user, secretKey);

        RefreshToken refreshToken = JwtManager.GenerateRefreshToken();
        JwtManager.SetRefreshToken(refreshToken, _httpContextAccessor.HttpContext, user);
        _repository.UpdateUser(user);
        _repository.Save();

        return token;
    }

    public string RefreshToken(string username, string secretKey)
    {
        var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
        var user = _repository.GetUserByRefreshToken(refreshToken);
        if (user == null || user.Username != username)
        {
            throw Exceptions.invalidRefreshToken;
        }
        else if (DateTime.Now > user.TokenExpires)
        {
            throw Exceptions.expiredRefreshToken;
        }

        string token = JwtManager.CreateToken(user, secretKey);

        var newRefreshToken = JwtManager.GenerateRefreshToken();
        JwtManager.SetRefreshToken(newRefreshToken, _httpContextAccessor.HttpContext, user);
        _repository.UpdateUser(user);
        _repository.Save();

        return token;
    }

    public void GiveRole(GiveRoleDto giveRoleDto)
    {
        giveRoleDto.Username = giveRoleDto.Username.Trim();
        giveRoleDto.RoleName = giveRoleDto.RoleName.Trim();

        var user = _repository.GetUserByUsername(giveRoleDto.Username);
        if (user == null)
        {
            throw Exceptions.userNotFound;
        }

        var role = _roleRepository.GetRoleByName(giveRoleDto.RoleName);
        if(role == null)
        {
            throw Exceptions.roleNotFound;
        }
        
        if(!user.Roles.Contains(role))
        {
            user.Roles.Add(role);
            _repository.Save();
        }
    }
}
