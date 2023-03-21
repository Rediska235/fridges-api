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
    public void GetAllProducts_ReturnsOkObjectResult_WithAllProducts()
    {
        // Arrange
        var products = _fixture.CreateMany<Product>();
        _productServiceMock.Setup(x => x.GetAllProducts()).Returns(products);

        // Act
        var result = controller.GetAllProducts();

        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(products, resultObject.Value);
    }

    [Fact]
    public void GetProductById_ReturnsOkObjectResult_WithSingleProduct()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productServiceMock.Setup(x => x.GetProductById(It.IsAny<Guid>())).Returns(product);

        // Act
        var result = controller.GetProductById(Guid.NewGuid());

        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(product, resultObject.Value);
    }

    [Fact]
    public void CreateProduct_ReturnsCreatedResult_WithCreatedProduct()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productServiceMock.Setup(x => x.CreateProduct(It.IsAny<CreateProductDto>())).Returns(product);

        // Act
        var result = controller.CreateProduct(_fixture.Create<CreateProductDto>());

        // Assert
        var resultObject = Assert.IsType<CreatedResult>(result);
        Assert.Equal(product, resultObject.Value);
    }

    [Fact]
    public void UpdateProduct_ReturnsOkObjectResult_WithUpdatedProduct()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _productServiceMock.Setup(x => x.UpdateProduct(It.IsAny<UpdateProductDto>())).Returns(product);

        // Act
        var result = controller.UpdateProduct(_fixture.Create<UpdateProductDto>());

        // Assert
        var resultObject = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(product, resultObject.Value);
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