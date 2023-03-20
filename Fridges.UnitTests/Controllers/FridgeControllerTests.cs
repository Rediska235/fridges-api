using AutoFixture;
using Fridges.API.Controllers;
using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Application.Services.Interfaces;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fridges.UnitTests.Controllers;

public class FridgeControllerTests
{
    private readonly Mock<IFridgeService> _fridgeServiceMock;
    private readonly Fixture _fixture;
    private readonly FridgeController controller;

    public FridgeControllerTests()
    {
        _fridgeServiceMock = new Mock<IFridgeService>();

        controller = new FridgeController(_fridgeServiceMock.Object);

        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public void GetAllFridges_ReturnsOkObjectResult()
    {
        // Arrange
        var fridges = _fixture.CreateMany<Fridge>();
        _fridgeServiceMock.Setup(x => x.GetAllFridges()).Returns(fridges);

        // Act
        var result = controller.GetAllFridges();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetAllFridges_ReturnsAllFridges()
    {
        // Arrange
        var fridges = _fixture.CreateMany<Fridge>();
        _fridgeServiceMock.Setup(x => x.GetAllFridges()).Returns(fridges);

        // Act
        var result = controller.GetAllFridges() as OkObjectResult;

        // Assert
        Assert.Equal(fridges, result.Value);
    }

    [Fact]
    public void GetProductsByFridgeId_ReturnsOkObjectResult()
    {
        // Arrange
        var productQuantities = _fixture.CreateMany<ProductQuantity>();
        _fridgeServiceMock.Setup(x => x.GetProductsByFridgeId(It.IsAny<Guid>())).Returns(productQuantities);

        // Act
        var result = controller.GetProductsByFridgeId(Guid.NewGuid());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public void GetProductsByFridgeId_ReturnsProductsByFridgeId()
    {
        // Arrange
        var fridge = _fixture.Create<Fridge>();
        var products = _fixture.CreateMany<ProductQuantity>();
        var fridgeId = Guid.NewGuid();
        _fridgeServiceMock.Setup(x => x.GetFridgeById(fridgeId)).Returns(fridge);
        _fridgeServiceMock.Setup(x => x.GetProductsByFridgeId(fridgeId)).Returns(products);

        // Act
        var result = controller.GetProductsByFridgeId(fridgeId) as OkObjectResult;
        var fridgeWithProductsFromTest = (FridgeWithProductsDto)result.Value;
        // Assert
        Assert.Equal(fridge, fridgeWithProductsFromTest.Fridge);
        Assert.Equal(products, fridgeWithProductsFromTest.Products);
    }

    [Fact]
    public void AddProducts_ReturnsOkResult()
    {
        // Arrange
        _fridgeServiceMock.Setup(x => x.AddProducts(It.IsAny<Guid>(), It.IsAny<AddProductsDto>()));

        // Act
        var result = controller.AddProducts(Guid.NewGuid(), new AddProductsDto());

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void RemoveProductFromFridge_ReturnsNoContentResult()
    {
        // Arrange
        _fridgeServiceMock.Setup(x => x.RemoveProducts(It.IsAny<Guid>(), It.IsAny<Guid>()));

        // Act
        var result = controller.RemoveProducts(Guid.NewGuid(), Guid.NewGuid());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void UpdateProductsQuantity_ReturnsOkObjectResult()
    {
        // Arrange

        // Act
        var result = controller.UpdateProductsQuantity();

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public void CreateFridge_ReturnsCreatedResult()
    {
        // Arrange
        var fridge = _fixture.Create<Fridge>();
        _fridgeServiceMock.Setup(x => x.CreateFridge(It.IsAny<CreateFridgeDto>())).Returns(fridge);

        // Act
        var result = controller.CreateFridge(new CreateFridgeDto());

        // Assert
        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public void CreateFridge_ReturnsCreatedFridge()
    {
        // Arrange
        var fridge = _fixture.Create<Fridge>();
        _fridgeServiceMock.Setup(x => x.CreateFridge(It.IsAny<CreateFridgeDto>())).Returns(fridge);

        // Act
        var result = controller.CreateFridge(new CreateFridgeDto()) as CreatedResult;

        // Assert
        Assert.Equal(fridge, result.Value);
    }

    [Fact]
    public void UpdateFridge_ReturnsOkObjectResult()
    {
        // Arrange
        var fridge = _fixture.Create<Fridge>();
        _fridgeServiceMock.Setup(x => x.UpdateFridge(It.IsAny<UpdateFridgeDto>())).Returns(fridge);

        // Act
        var result = controller.UpdateFridge(new UpdateFridgeDto());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void UpdateFridge_ReturnsUpdatedFridge()
    {
        // Arrange
        var fridge = _fixture.Create<Fridge>();
        _fridgeServiceMock.Setup(x => x.UpdateFridge(It.IsAny<UpdateFridgeDto>())).Returns(fridge);

        // Act
        var result = controller.UpdateFridge(new UpdateFridgeDto()) as OkObjectResult;

        // Assert
        Assert.Equal(fridge, result.Value);
    }

    [Fact]
    public void DeleteFridge_ReturnsNoContentResult()
    {
        // Arrange
        _fridgeServiceMock.Setup(x => x.DeleteFridge(It.IsAny<Guid>()));

        // Act
        var result = controller.DeleteFridge(Guid.NewGuid());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
