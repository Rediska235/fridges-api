using AutoFixture;
using Fridges.API.Controllers;
using Fridges.Application.DTOs;
using Fridges.Application.Services.Interfaces;
using Fridges.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Fridges.UnitTests.Controllers;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly Mock<IConfiguration> _configuration;
    private readonly Mock<IConfigurationSection> _configurationSection;
    private readonly Fixture _fixture;
    private readonly AuthController controller;

    public AuthControllerTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        _configuration = new Mock<IConfiguration>();
        _configurationSection = new Mock<IConfigurationSection>();
        
        controller = new AuthController(_authServiceMock.Object, _configuration.Object);

        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public void Register_ReturnsOkObjectResult_WithUser()
    {
        // Arrange
        var user = _fixture.Create<User>();
        _authServiceMock.Setup(x => x.Register(It.IsAny<UserDto>())).Returns(user);

        // Act
        var result = controller.Register(new UserDto());

        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(user, resultObject.Value);
    }

    [Fact]
    public void Login_ReturnsOkObjectResult_WithToken()
    {
        // Arrange
        var token = "jwt";
        _authServiceMock.Setup(x => x.Login(It.IsAny<UserDto>(), It.IsAny<string>())).Returns(token);
        _configurationSection.Setup(x => x.Value).Returns("some string");
        _configuration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(_configurationSection.Object);

        // Act
        var result = controller.Login(new UserDto());

        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(token, resultObject.Value);
    }

    [Fact]
    public void RefreshToken_ReturnsOkObjectResult_WithToken()
    {
        // Arrange
        var token = "jwt";
        _authServiceMock.Setup(x => x.RefreshToken(It.IsAny<string>())).Returns(token);
        _configurationSection.Setup(x => x.Value).Returns("some string");
        _configuration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(_configurationSection.Object);

        // Act
        var result = controller.RefreshToken();

        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(token, resultObject.Value);
    }

    [Fact]
    public void GiveRole_ReturnsOkResult()
    {
        // Arrange
        _authServiceMock.Setup(x => x.GiveRole(It.IsAny<GiveRoleDto>()));

        // Act
        var result = controller.GiveRole(new GiveRoleDto());

        // Assert
        Assert.IsType<OkResult>(result);
    }
}
