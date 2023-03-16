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

    public void CreateProduct(Product Product)
    {
        _repository.InsertProduct(Product);
    }

    public void UpdateProduct(Product Product)
    {
        _repository.UpdateProduct(Product);
    }

    public void DeleteProduct(Guid ProductId)
    {
        _repository.DeleteProduct(ProductId);
    }
}
