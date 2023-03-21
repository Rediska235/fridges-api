using AutoFixture;
using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Application.Repositories;
using Fridges.Application.Services.Implementations;
using Fridges.Domain.Entities;
using Fridges.Domain.Exceptions;
using Moq;

namespace Fridges.UnitTests.Services;

public class FridgeServiceTests
{
    private readonly Mock<IFridgeRepository> _fridgeRepository;
    private readonly Mock<IFridgeModelRepository> _fridgeModelRepository;
    private readonly Mock<IProductRepository> _productRepository;
    private readonly Mock<IFridgeProductRepository> _fridgeProductRepository;
    private readonly Fixture _fixture;
    private readonly FridgeService service;

    public FridgeServiceTests()
    {
        _fridgeRepository = new Mock<IFridgeRepository>();
        _fridgeModelRepository = new Mock<IFridgeModelRepository>();
        _productRepository = new Mock<IProductRepository>();
        _fridgeProductRepository = new Mock<IFridgeProductRepository>();

        service = new FridgeService(
            _fridgeRepository.Object, 
            _fridgeModelRepository.Object, 
            _productRepository.Object, 
            _fridgeProductRepository.Object);

        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public void GetAllFridges_ReturnsAllFridges()
    {
        // Arrange
        var fridges = _fixture.CreateMany<Fridge>();
        _fridgeRepository.Setup(x => x.GetFridges()).Returns(fridges);

        // Act
        var result = service.GetAllFridges();

        // Assert
        Assert.Equal(fridges, result);
    }

    [Fact]
    public void GetFridgeById_ReturnsFridge()
    {
        // Arrange
        var fridge = _fixture.Create<Fridge>();
        _fridgeRepository.Setup(x => x.GetFridgeById(It.IsAny<Guid>())).Returns(fridge);

        // Act
        var result = service.GetFridgeById(Guid.NewGuid());

        // Assert
        Assert.Equal(fridge, result);
    }

    [Fact]
    public void AddProducts_ThrowsException_WhenNegativeProductsQuantity()
    {
        // Arrange
        var fridgeProduct = _fixture.Create<FridgeProduct>();
        var addProductsDto = _fixture.Create<AddProductsDto>();
        fridgeProduct.Quantity = -1;
        addProductsDto.Quantity = 0;
        _fridgeProductRepository.Setup(x => x.GetFridgeProductByIds(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(fridgeProduct);

        // Act

        // Assert
        Assert.Throws<NotAllowedException>(() => service.AddProducts(Guid.NewGuid(), addProductsDto));
    }

    [Fact]
    public void AddProducts_ThrowsException_WhenNegativeProductsQuantityAndItsNewProduct()
    {
        // Arrange
        var addProductsDto = _fixture.Create<AddProductsDto>();
        addProductsDto.Quantity = -1;
        _fridgeProductRepository.Setup(x => x.GetFridgeProductByIds(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns((FridgeProduct)null);

        // Act

        // Assert
        Assert.Throws<NotAllowedException>(() => service.AddProducts(Guid.NewGuid(), addProductsDto));
    }

    [Fact]
    public void AddProducts_Successful()
    {
        // Arrange
        var fridgeProduct = _fixture.Create<FridgeProduct>();
        var addProductsDto = _fixture.Create<AddProductsDto>();
        fridgeProduct.Quantity = 1;
        addProductsDto.Quantity = 0;
        _fridgeProductRepository.Setup(x => x.GetFridgeProductByIds(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(fridgeProduct);

        // Act
        var exception = Record.Exception(() => service.AddProducts(Guid.NewGuid(), addProductsDto));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void RemoveProducts_Successful()
    {
        // Arrange

        // Act
        var exception = Record.Exception(() => service.RemoveProducts(Guid.NewGuid(), Guid.NewGuid()));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void CreateFridge_ThrowsException_WhenAlreadyExists()
    {
        // Arrange
        var fridge = _fixture.Create<Fridge>();
        _fridgeRepository.Setup(x => x.GetFridgeByName(It.IsAny<string>())).Returns(fridge);

        // Act

        // Assert
        Assert.Throws<AlreadyExistsException>(() => service.CreateFridge(_fixture.Create<CreateFridgeDto>()));
    }

    [Fact]
    public void CreateFridge_Successful()
    {
        // Arrange
        _fridgeRepository.Setup(x => x.GetFridgeByName(It.IsAny<string>())).Throws(Exceptions.fridgeNotFound);
        
        // Act
        var exception = Record.Exception(() => service.CreateFridge(_fixture.Create<CreateFridgeDto>()));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void UpdateFridge_ThrowsException_WhenAlreadyExists()
    {
        // Arrange
        var fridge = _fixture.Create<Fridge>();
        _fridgeRepository.Setup(x => x.GetFridgeById(It.IsAny<Guid>())).Returns(fridge);
        _fridgeRepository.Setup(x => x.GetFridgeByName(It.IsAny<string>())).Returns(fridge);

        // Act

        // Assert
        Assert.Throws<AlreadyExistsException>(() => service.UpdateFridge(_fixture.Create<UpdateFridgeDto>()));
    }

    [Fact]
    public void UpdateFridge_Successful()
    {
        // Arrange
        var fridge = _fixture.Create<Fridge>();
        _fridgeRepository.Setup(x => x.GetFridgeById(It.IsAny<Guid>())).Returns(fridge);
        _fridgeRepository.Setup(x => x.GetFridgeByName(It.IsAny<string>())).Throws(Exceptions.fridgeNotFound);

        // Act
        var exception = Record.Exception(() => service.UpdateFridge(_fixture.Create<UpdateFridgeDto>()));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void DeleteFridge_Successful()
    {
        // Arrange

        // Act
        var exception = Record.Exception(() => service.DeleteFridge(_fixture.Create<Guid>()));

        // Assert
        Assert.Null(exception);
    }
}