using AutoFixture;
using Fridges.Application.DTOs;
using Fridges.Application.Repositories;
using Fridges.Application.Services.Implementations;
using Fridges.Domain.Entities;
using Fridges.Domain.Exceptions;
using Moq;

namespace Fridges.UnitTests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly Fixture _fixture;
    private readonly ProductService service;

    public ProductServiceTests()
    {
        _productRepository = new Mock<IProductRepository>();

        service = new ProductService(_productRepository.Object);

        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public void GetAllProducts_ReturnsAllProducts()
    {
        // Arrange
        var products = _fixture.CreateMany<Product>();
        _productRepository.Setup(x => x.GetProducts()).Returns(products);

        // Act
        var result = service.GetAllProducts();

        // Assert
        Assert.Equal(products, result);
    }

    [Fact]
    public void GetProductById_ReturnsProduct()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productRepository.Setup(x => x.GetProductById(It.IsAny<Guid>())).Returns(product);

        // Act
        var result = service.GetProductById(Guid.NewGuid());

        // Assert
        Assert.Equal(product, result);
    }

    [Fact]
    public void CreateProduct_ThrowsException_WhenAlreadyExists()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productRepository.Setup(x => x.GetProductByName(It.IsAny<string>())).Returns(product);

        // Act

        // Assert
        Assert.Throws<AlreadyExistsException>(() => service.CreateProduct(_fixture.Create<CreateProductDto>()));
    }

    [Fact]
    public void CreateProduct_Successful()
    {
        // Arrange
        _productRepository.Setup(x => x.GetProductByName(It.IsAny<string>())).Throws(Exceptions.productNotFound);

        // Act
        var exception = Record.Exception(() => service.CreateProduct(_fixture.Create<CreateProductDto>()));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void UpdateProduct_ThrowsException_WhenAlreadyExists()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productRepository.Setup(x => x.GetProductById(It.IsAny<Guid>())).Returns(product);
        _productRepository.Setup(x => x.GetProductByName(It.IsAny<string>())).Returns(product);

        // Act

        // Assert
        Assert.Throws<AlreadyExistsException>(() => service.UpdateProduct(_fixture.Create<UpdateProductDto>()));
    }

    [Fact]
    public void UpdateProduct_Successful()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productRepository.Setup(x => x.GetProductById(It.IsAny<Guid>())).Returns(product);
        _productRepository.Setup(x => x.GetProductByName(It.IsAny<string>())).Throws(Exceptions.productNotFound);

        // Act
        var exception = Record.Exception(() => service.UpdateProduct(_fixture.Create<UpdateProductDto>()));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void DeleteProduct_Successful()
    {
        // Arrange

        // Act
        var exception = Record.Exception(() => service.DeleteProduct(_fixture.Create<Guid>()));

        // Assert
        Assert.Null(exception);
    }
}
