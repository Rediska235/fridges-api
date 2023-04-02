using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Application.Repositories;
using Fridges.Application.Services.Interfaces;
using Fridges.Domain.Entities;
using Fridges.Domain.Exceptions;

namespace Fridges.Application.Services.Implementations;

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

    public Product GetProductById(Guid productId)
    {
        return _repository.GetProductById(productId);
    }

    public Product CreateProduct(CreateProductDto createProductDto)
    {
        var productName = createProductDto.Name.Trim();
        if(AlreadyExists(productName))
        {
            throw Exceptions.productAlreadyExists;
        }

        var product = new Product()
        {
            Id = Guid.NewGuid(),
            Name = productName,
            DefaultQuantity = createProductDto.DefaultQuantity
        };

        _repository.InsertProduct(product);
        _repository.Save();

        return product;
    }

    public Product UpdateProduct(UpdateProductDto updateProductDto)
    {
        var product = _repository.GetProductById(updateProductDto.Id);

        var productName = updateProductDto.Name.Trim();
        if (productName != product.Name && AlreadyExists(productName))
        {
            throw Exceptions.productAlreadyExists;
        }

        product.Name = productName;
        product.DefaultQuantity = updateProductDto.DefaultQuantity;

        _repository.UpdateProduct(product);
        _repository.Save();

        return product;
    }

    public void DeleteProduct(Guid productId)
    {
        _repository.DeleteProduct(productId);
        _repository.Save();
    }

    private bool AlreadyExists(string name)
    {
        try
        {
            _repository.GetProductByName(name);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
