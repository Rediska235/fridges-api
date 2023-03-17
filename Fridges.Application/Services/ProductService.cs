using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Application.Interfaces.Repositories;
using Fridges.Application.Interfaces.Services;
using Fridges.Domain.Entities;

namespace Fridges.Application.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _repository.GetProducts();
    }

    public Product GetProductById(Guid ProductId)
    {
        return _repository.GetProductById(ProductId);
    }

    public Product CreateProduct(CreateProductDto Product)
    {
        var product = new Product()
        {
            Id = new Guid(),
            Name = Product.Name,
            DefaultQuantity = Product.DefaultQuantity
        };

        _repository.InsertProduct(product);
        _repository.Save();

        return product;
    }

    public Product UpdateProduct(UpdateProductDto updateProductDto)
    {
        var product = new Product()
        {
            Id = updateProductDto.Id,
            Name = updateProductDto.Name,
            DefaultQuantity = updateProductDto.DefaultQuantity,
            FridgeProducts = null
        };

        _repository.UpdateProduct(product);
        _repository.Save();

        return _repository.GetProductById(product.Id);
    }

    public void DeleteProduct(Guid ProductId)
    {
        _repository.DeleteProduct(ProductId);
        _repository.Save();
    }
}
