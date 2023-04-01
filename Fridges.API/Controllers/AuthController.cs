using Fridges.Application.DTOs;
using Fridges.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("refresh-token"), Authorize(AuthenticationSchemes = "ExpiredTokenAllowed")]
    public IActionResult RefreshToken()
    {
        var username = User.Identity.Name;
        var secretKey = _configuration.GetSection("JWT:Key").Value;
        var jwtToken = _authService.RefreshToken(username, secretKey);

        return Ok(jwtToken);
    }

    [HttpPost("give-role"), Authorize(Roles = "Admin")]
    public IActionResult GiveRole(GiveRoleDto giveRoleDto)
    {
        _authService.GiveRole(giveRoleDto);

        return Ok();
    }

    [HttpGet("users"), Authorize(Roles = "Admin")]
    public IActionResult GetAllUsers()
    {
        return Ok(_authService.GetAllUsers());
    }

    [HttpGet("roles"), Authorize(Roles = "Admin")]
    public IActionResult GetAllRoles()
    {
        return Ok(_authService.GetAllRoles());
    }
}
