using Fridges.API.DTOs;
using Fridges.Application.DTOs;
using Fridges.Application.Repositories;
using Fridges.Application.Services.Services;
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

    public Product GetProductById(Guid ProductId)
    {
        return _repository.GetProductById(ProductId);
    }

    public Product CreateProduct(CreateProductDto Product)
    {
        Product.Name = Product.Name.Trim();
        if(AlreadyExists(Product.Name))
        {
            throw Exceptions.productAlreadyExists;
        }

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
