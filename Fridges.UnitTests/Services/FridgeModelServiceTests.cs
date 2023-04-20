using AutoFixture;
using Fridges.Application.DTOs;
using Fridges.Application.Repositories;
using Fridges.Application.Services.Implementations;
using Fridges.Domain.Entities;
using Fridges.Domain.Exceptions;
using Moq;

namespace Fridges.UnitTests.Services;

public class FridgeModelServiceTests
{
    private readonly Mock<IFridgeModelRepository> _fridgeModelRepository;
    private readonly Fixture _fixture;
    private readonly FridgeModelService service;

    public FridgeModelServiceTests()
    {
        _fridgeModelRepository = new Mock<IFridgeModelRepository>();

        service = new FridgeModelService(_fridgeModelRepository.Object);

        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public void GetAllFridgeModels_ReturnsAllProducts()
    {
        // Arrange
        var fridgeModels = _fixture.CreateMany<FridgeModel>();
        _fridgeModelRepository.Setup(x => x.GetFridgeModels()).Returns(fridgeModels);

        // Act
        var result = service.GetAllFridgeModels();

        // Assert
        Assert.Equal(fridgeModels, result);
    }

    [Fact]
    public void GetFridgeModelById_ReturnsFridgeModel()
    {
        // Arrange
        var fridgeModel = _fixture.Create<FridgeModel>();
        _fridgeModelRepository.Setup(x => x.GetFridgeModelById(It.IsAny<Guid>())).Returns(fridgeModel);

        // Act
        var result = service.GetFridgeModelById(Guid.NewGuid());

        // Assert
        Assert.Equal(fridgeModel, result);
    }

    [Fact]
    public void CreateFridgeModel_ThrowsException_WhenAlreadyExists()
    {
        // Arrange
        var fridgeModel = _fixture.Create<FridgeModel>();
        _fridgeModelRepository.Setup(x => x.GetFridgeModelByName(It.IsAny<string>())).Returns(fridgeModel);

        // Act

        // Assert
        Assert.Throws<AlreadyExistsException>(() => service.CreateFridgeModel(_fixture.Create<CreateFridgeModelDto>()));
    }

    [Fact]
    public void CreateFridgeModel_Successful()
    {
        // Arrange
        _fridgeModelRepository.Setup(x => x.GetFridgeModelByName(It.IsAny<string>())).Throws(Exceptions.fridgeModelNotFound);

        // Act
        var exception = Record.Exception(() => service.CreateFridgeModel(_fixture.Create<CreateFridgeModelDto>()));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void UpdateFridgeModel_ThrowsException_WhenAlreadyExists()
    {
        // Arrange
        var fridgeModel = _fixture.Create<FridgeModel>();
        _fridgeModelRepository.Setup(x => x.GetFridgeModelById(It.IsAny<Guid>())).Returns(fridgeModel);
        _fridgeModelRepository.Setup(x => x.GetFridgeModelByName(It.IsAny<string>())).Returns(fridgeModel);

        // Act

        // Assert
        Assert.Throws<AlreadyExistsException>(() => service.UpdateFridgeModel(_fixture.Create<UpdateFridgeModelDto>()));
    }

    [Fact]
    public void UpdateFridgeModel_Successful()
    {
        // Arrange
        var fridgeModel = _fixture.Create<FridgeModel>();
        _fridgeModelRepository.Setup(x => x.GetFridgeModelById(It.IsAny<Guid>())).Returns(fridgeModel);
        _fridgeModelRepository.Setup(x => x.GetFridgeModelByName(It.IsAny<string>())).Throws(Exceptions.fridgeModelNotFound);

        // Act
        var exception = Record.Exception(() => service.UpdateFridgeModel(_fixture.Create<UpdateFridgeModelDto>()));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void DeleteFridgeModel_Succesful()
    {
        // Arrange

        // Act
        var exception = Record.Exception(() => service.DeleteFridgeModel(_fixture.Create<Guid>()));

        // Assert
        Assert.Null(exception);
    }
}
