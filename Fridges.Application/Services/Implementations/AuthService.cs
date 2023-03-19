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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IUserRepository repository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
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
            Id = new Guid(),
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

    public void Logout()
    {
        throw new NotImplementedException();
    }

    public string RefreshToken(string secretKey)
    {
        var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
        var user = _repository.GetUserByRefreshToken(refreshToken);
        if (user == null)
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

}
