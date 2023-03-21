using AutoFixture;
using Fridges.API.Controllers;
using Fridges.Application.DTOs;
using Fridges.Application.Services.Interfaces;
using Fridges.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fridges.UnitTests.Controllers;

public class FridgeModelControllerTests
{
    private readonly Mock<IFridgeModelService> _fridgeModelServiceMock;
    private readonly Fixture _fixture;
    private readonly FridgeModelController controller;

    public FridgeModelControllerTests()
    {
        _fridgeModelServiceMock = new Mock<IFridgeModelService>();

        controller = new FridgeModelController(_fridgeModelServiceMock.Object);

        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public void GetAllFridgeModels_ReturnsOkObjectResult_WithAllFridgeModels()
    {
        // Arrange
        var fridgeModels = _fixture.CreateMany<FridgeModel>();
        _fridgeModelServiceMock.Setup(x => x.GetAllFridgeModels()).Returns(fridgeModels);

        // Act
        var result = controller.GetAllFridgeModels();

        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(fridgeModels, resultObject.Value);
    }

    [Fact]
    public void GetFridgeModelById_ReturnsOkObjectResult_WithSingleFridgeModel()
    {
        // Arrange
        var fridgeModel = _fixture.Create<FridgeModel>();
        _fridgeModelServiceMock.Setup(x => x.GetFridgeModelById(It.IsAny<Guid>())).Returns(fridgeModel);

        // Act
        var result = controller.GetFridgeModelById(Guid.NewGuid());

        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(fridgeModel, resultObject.Value);
    }

    [Fact]
    public void CreateFridgeModel_ReturnsCreatedResult_WithCreatedFridgeModel()
    {
        // Arrange
        var fridgeModel = _fixture.Create<FridgeModel>();
        _fridgeModelServiceMock.Setup(x => x.CreateFridgeModel(It.IsAny<CreateFridgeModelDto>())).Returns(fridgeModel);

        // Act
        var result = controller.CreateFridgeModel(new CreateFridgeModelDto());

        // Assert
        var resultObject = Assert.IsType<CreatedResult>(result);
        Assert.Equal(fridgeModel, resultObject.Value);
    }

    [Fact]
    public void UpdateFridgeModel_ReturnsOkObjectResult_WithUpdatedFridgeModel()
    {
        // Arrange
        var fridgeModel = _fixture.Create<FridgeModel>();
        _fridgeModelServiceMock.Setup(x => x.UpdateFridgeModel(It.IsAny<UpdateFridgeModelDto>())).Returns(fridgeModel);

        // Act
        var result = controller.UpdateFridgeModel(new UpdateFridgeModelDto());

        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(fridgeModel, resultObject.Value);
    }

    [Fact]
    public void DeleteFridgeModel_ReturnsNoContentResult()
    {
        // Arrange

        // Act
        var result = controller.DeleteFridgeModel(Guid.NewGuid());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
