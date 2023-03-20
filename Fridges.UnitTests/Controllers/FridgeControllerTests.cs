using AutoFixture;
using Fridges.API.Controllers;
using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Application.Services.Interfaces;
using Fridges.Domain.DTOs;
using Fridges.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fridges.UnitTests.Controllers;

public class FridgeControllerTests
{
    private readonly Mock<IFridgeService> _fridgeServiceMock;
    private readonly Fixture _fixture;

    public FridgeControllerTests()
    {
        _fridgeServiceMock = new Mock<IFridgeService>();

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

        var controller = new FridgeController(_fridgeServiceMock.Object);

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

        var controller = new FridgeController(_fridgeServiceMock.Object);

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

        var controller = new FridgeController(_fridgeServiceMock.Object);

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
        var productQuantities = _fixture.CreateMany<ProductQuantity>();
        _fridgeServiceMock.Setup(x => x.GetFridgeById(It.IsAny<Guid>())).Returns(fridge);
        _fridgeServiceMock.Setup(x => x.GetProductsByFridgeId(It.IsAny<Guid>())).Returns(productQuantities);
        
        var fridgeWithProducts = new FridgeWithProductsDto
        {
            Fridge = fridge,
            Products = productQuantities
        };
        
        var controller = new FridgeController(_fridgeServiceMock.Object);

        // Act
        var result = controller.GetProductsByFridgeId(Guid.NewGuid()) as OkObjectResult;

        // Assert
        Assert.Equal(fridgeWithProducts, result.Value);
    }

    [Fact]
    public void AddProducts_ReturnsOkObjectResult()
    {
        // Arrange
        _fridgeServiceMock.Setup(x => x.AddProducts(It.IsAny<Guid>(), It.IsAny<AddProductsDto>()));

        var controller = new FridgeController(_fridgeServiceMock.Object);

        // Act
        var result = controller.AddProducts(Guid.NewGuid(), new AddProductsDto());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void RemoveProductFromFridge_ReturnsNoContentResult()
    {
        // Arrange
        var productQuantity = _fixture.Create<ProductQuantity>();
        _fridgeServiceMock.Setup(x => x.RemoveProducts(It.IsAny<Guid>(), It.IsAny<Guid>()));

        var controller = new FridgeController(_fridgeServiceMock.Object);

        // Act
        var result = controller.RemoveProducts(Guid.NewGuid(), Guid.NewGuid());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void UpdateProductsQuantity_ReturnsOkObjectResult()
    {
        // Arrange
        var controller = new FridgeController(_fridgeServiceMock.Object);

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

        var controller = new FridgeController(_fridgeServiceMock.Object);

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

        var controller = new FridgeController(_fridgeServiceMock.Object);

        // Act
        var result = controller.CreateFridge(new CreateFridgeDto()) as CreatedResult;

        // Assert
        Assert.Equal(fridge, result.Value);
    }

    [Fact]
    public void DeleteFridge_ReturnsNoContentResult()
    {
        // Arrange
        _fridgeServiceMock.Setup(x => x.DeleteFridge(It.IsAny<Guid>()));

        var controller = new FridgeController(_fridgeServiceMock.Object);

        // Act
        var result = controller.DeleteFridge(Guid.NewGuid());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
