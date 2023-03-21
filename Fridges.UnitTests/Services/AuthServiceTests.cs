using AutoFixture;
using BCrypt.Net;
using Fridges.Application.DTOs;
using Fridges.Application.Repositories;
using Fridges.Application.Services.Implementations;
using Fridges.Domain.Entities;
using Fridges.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Authentication;

namespace Fridges.UnitTests.Services;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IRoleRepository> _roleRepository;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
    private readonly Fixture _fixture;
    private readonly AuthService service;

    public AuthServiceTests()
    {
        _userRepository = new Mock<IUserRepository>();
        _roleRepository = new Mock<IRoleRepository>();
        _httpContextAccessor = new Mock<IHttpContextAccessor>();

        service = new AuthService(_userRepository.Object, _roleRepository.Object, _httpContextAccessor.Object);

        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public void Register_ThrowsException_WhenUsernameIsTaken()
    {
        // Arrange
        var user = _fixture.Create<User>();
        _userRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);

        // Act

        // Assert
        Assert.Throws<AlreadyExistsException>(() => service.Register(_fixture.Create<UserDto>()));
    }

    [Fact]
    public void Register_Successful()
    {
        // Arrange
        _userRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns((User)null);

        // Act
        var exception = Record.Exception(() => service.Register(_fixture.Create<UserDto>()));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Login_ThrowsException_WhenUsernameNotFound()
    {
        // Arrange
        _userRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns((User)null);

        // Act

        // Assert
        Assert.Throws<InvalidCredentialException>(() => service.Login(_fixture.Create<UserDto>(), _fixture.Create<string>()));
    }

    [Fact]
    public void Login_ThrowsException_WhenInvalidPassword()
    {
        // Arrange
        var user = _fixture.Create<User>();
        user.PasswordHash = "$2a$12$UfkQ1pm1u651uI/XLKMf1eYqhJd3aqPzjqY3ehEjqQqHYNWDRXHPa";
        _userRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);

        // Act

        // Assert
        Assert.Throws<InvalidCredentialException>(() => service.Login(_fixture.Create<UserDto>(), _fixture.Create<string>()));
    }

    [Fact]
    public void Login_Successful()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var userDto = _fixture.Create<UserDto>();
        userDto.Password = "qwerty";
                            //bcrypt hash of "qwerty"
        user.PasswordHash = "$2a$12$Jp4.xazH7vY.0mHMSobdhe4UlVRsIRyY.WO/uhK4NKbMqRlwAr9KO";
        _userRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);
        _httpContextAccessor.Setup(x => x.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>()));

        // Act
        var exception = Record.Exception(() => service.Login(userDto, _fixture.Create<string>()));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void RefreshToken_ThrowsException_WhenUserNotFound()
    {
        // Arrange
        _userRepository.Setup(x => x.GetUserByRefreshToken(It.IsAny<string>())).Returns((User)null);
         _httpContextAccessor.Setup(x => x.HttpContext.Request.Cookies["refreshToken"]).Returns(_fixture.Create<string>());

        // Act

        // Assert
        Assert.Throws<InvalidCredentialException>(() => service.RefreshToken(_fixture.Create<string>()));
    }

    [Fact]
    public void RefreshToken_ThrowsException_WhenTokenExpired()
    {
        // Arrange
        var user = _fixture.Create<User>();
        user.TokenExpires = DateTime.Now.AddDays(-1);
        _userRepository.Setup(x => x.GetUserByRefreshToken(It.IsAny<string>())).Returns(user);
        _httpContextAccessor.Setup(x => x.HttpContext.Request.Cookies["refreshToken"]).Returns(_fixture.Create<string>());

        // Act

        // Assert
        Assert.Throws<InvalidCredentialException>(() => service.RefreshToken(_fixture.Create<string>()));
    }

    [Fact]
    public void RefreshToken_Successful()
    {
        // Arrange
        var user = _fixture.Create<User>();
        user.TokenExpires = DateTime.Now.AddDays(1);
        _userRepository.Setup(x => x.GetUserByRefreshToken(It.IsAny<string>())).Returns(user);
        _httpContextAccessor.Setup(x => x.HttpContext.Request.Cookies["refreshToken"]).Returns(_fixture.Create<string>());
        _httpContextAccessor.Setup(x => x.HttpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>()));

        // Act
        var exception = Record.Exception(() => service.RefreshToken(_fixture.Create<string>()));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void GiveRole_ThrowsException_WhenUserNotFound()
    {
        // Arrange
        _userRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns((User)null);

        // Act

        // Assert
        Assert.Throws<NotFoundException>(() => service.GiveRole(_fixture.Create<GiveRoleDto>()));
    }

    [Fact]
    public void GiveRole_ThrowsException_WhenRoleNotFound()
    {
        // Arrange
        var user = _fixture.Create<User>();
        _userRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);
        _roleRepository.Setup(x => x.GetRoleByName(It.IsAny<string>())).Returns((Role)null);

        // Act

        // Assert
        Assert.Throws<NotFoundException>(() => service.GiveRole(_fixture.Create<GiveRoleDto>()));
    }

    [Fact]
    public void GiveRole_Successful()
    {
        // Arrange
        var user = _fixture.Create<User>();
        var role = _fixture.Create<Role>();
        _userRepository.Setup(x => x.GetUserByUsername(It.IsAny<string>())).Returns(user);
        _roleRepository.Setup(x => x.GetRoleByName(It.IsAny<string>())).Returns(role);

        // Act
        var exception = Record.Exception(() => service.GiveRole(_fixture.Create<GiveRoleDto>()));

        // Assert
        Assert.Null(exception);
    }
}