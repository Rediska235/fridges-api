using AutoFixture;
using Fridges.API.Controllers;
using Fridges.Application.DTOs;
using Fridges.Application.Services.Interfaces;
using Fridges.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fridges.UnitTests.Controllers;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _productServiceMock;
    private readonly Fixture _fixture;
    private readonly ProductController controller;

    public ProductControllerTests()
    {
        _productServiceMock = new Mock<IProductService>();

        controller = new ProductController(_productServiceMock.Object);

        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public void GetAllProducts_ReturnsOkObjectResult()
    {
        // Arrange
        var products = _fixture.CreateMany<Product>();
        _productServiceMock.Setup(x => x.GetAllProducts()).Returns(products);

        // Act
        var result = controller.GetAllProducts();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetAllProducts_ReturnsAllProducts()
    {
        // Arrange
        var products = _fixture.CreateMany<Product>();
        _productServiceMock.Setup(x => x.GetAllProducts()).Returns(products);

        // Act
        var result = controller.GetAllProducts() as OkObjectResult;

        // Assert
        Assert.Equal(products, result.Value);
    }

    [Fact]
    public void GetProductById_ReturnsOkObjectResult()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productServiceMock.Setup(x => x.GetProductById(It.IsAny<Guid>())).Returns(product);

        // Act
        var result = controller.GetProductById(Guid.NewGuid());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetProductById_ReturnsProduct()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productServiceMock.Setup(x => x.GetProductById(It.IsAny<Guid>())).Returns(product);

        // Act
        var result = controller.GetProductById(Guid.NewGuid()) as OkObjectResult;

        // Assert
        Assert.Equal(product, result.Value);
    }

    [Fact]
    public void CreateProduct_ReturnsCreatedResult()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productServiceMock.Setup(x => x.CreateProduct(It.IsAny<CreateProductDto>())).Returns(product);

        // Act
        var result = controller.CreateProduct(_fixture.Create<CreateProductDto>());

        // Assert
        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public void CreateProduct_ReturnsProduct()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productServiceMock.Setup(x => x.CreateProduct(It.IsAny<CreateProductDto>())).Returns(product);

        // Act
        var result = controller.CreateProduct(_fixture.Create<CreateProductDto>()) as CreatedResult;

        // Assert
        Assert.Equal(product, result.Value);
    }

    [Fact]
    public void UpdateProduct_ReturnsOkObjectResult()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productServiceMock.Setup(x => x.UpdateProduct(It.IsAny<UpdateProductDto>())).Returns(product);

        // Act
        var result = controller.UpdateProduct(_fixture.Create<UpdateProductDto>());

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void UpdateProduct_ReturnsProduct()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productServiceMock.Setup(x => x.UpdateProduct(It.IsAny<UpdateProductDto>())).Returns(product);

        // Act
        var result = controller.UpdateProduct(_fixture.Create<UpdateProductDto>()) as OkObjectResult;

        // Assert
        Assert.Equal(product, result.Value);
    }

    [Fact]
    public void DeleteProduct_ReturnsNoContentResult()
    {
        // Arrange

        // Act
        var result = controller.DeleteProduct(Guid.NewGuid());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}