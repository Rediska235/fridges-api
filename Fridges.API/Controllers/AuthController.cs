using Fridges.Application.DTOs;
using Fridges.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.API.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public IActionResult Register(UserDto request)
    {
        var user = _authService.Register(request);

        return Ok(user);
    }

    [HttpPost("login")]
    public IActionResult Login(UserDto request)
    {
        string secretKey = _configuration.GetSection("JWT:Key").Value;
        var token = _authService.Login(request, secretKey);

        return Ok(token);
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        throw new NotImplementedException();
    }

    [HttpPost("admin")]
    public IActionResult GetAdminRights(string adminKey)
    {
        throw new NotImplementedException();
    }

    [HttpGet("refresh-token")]
    public IActionResult RefreshToken()
    {
        string secretKey = _configuration.GetSection("JWT:Key").Value;
        string refreshToken = _authService.RefreshToken(secretKey);

        return Ok(refreshToken);
    }
}
